using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YK.Console.Business.Abstractors;

namespace YK.Console.Business.UserStaffInfos;

internal class UserStaffInfoHandler(IReadRepository<UserStaffInfo> _repo) : IRequestHandler<UserStaffInfoRequest, UserStaffOutput>
{
    public async Task<UserStaffOutput> Handle(UserStaffInfoRequest request, CancellationToken cancellationToken)
         => await _repo.SimpleSingleAsync<UserStaffOutput>(x => x.Id == request.Id, cancellationToken)
                ?? throw ResultOutput.Exception("员工不存在");
}

internal class GetUserStaffExtendHandler(IReadRepository<UserStaffOrg> _staffOrgRepo, IReadRepository<UserStaffRole> _staffRoleRepo)
    : IRequestHandler<GetUserStaffExtendRequest, UserStaffExtendOutput>
{
    public async Task<UserStaffExtendOutput> Handle(GetUserStaffExtendRequest request, CancellationToken cancellationToken)
    {
        var result = new UserStaffExtendOutput();

        result.OrgIds = await _staffOrgRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.UserStaffId == request.Id)
            .Select(x => x.OrgId)
            .ToListAsync();

        result.RoleIds = await _staffRoleRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.UserStaffId == request.Id)
            .Select(x => x.RoleId)
            .ToListAsync();

        return result;
    }
}

internal class CreateUserStaffHandler(IUserStaffOperation _operation) : IRequestHandler<CreateUserStaffRequest, Guid>
{
    public Task<Guid> Handle(CreateUserStaffRequest request, CancellationToken cancellationToken)
        => _operation.CreateAsync(request, cancellationToken);
}

internal class UpdatePersonalUserStaffHandler(ICurrentUser _currentUser, IRepository<UserStaffInfo> _repo) : IRequestHandler<UpdatePersonalUserStaffReuqest, Guid>
{
    public async Task<Guid> Handle(UpdatePersonalUserStaffReuqest request, CancellationToken cancellationToken)
    {
        //保存员工
        var entity = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true).GetByIdAsync(_currentUser.UserStaffId??Guid.Empty, cancellationToken)
             ?? throw ResultOutput.Exception("员工不存在");
        entity.Update(realName: request.RealName, remark: request.Remark);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateUserStaffHandler(IUserStaffOperation _operation) : IRequestHandler<UpdateUserStaffRequest, Guid>
{
    public Task<Guid> Handle(UpdateUserStaffRequest request, CancellationToken cancellationToken)
        =>_operation.UpdateAsync(request, cancellationToken);
}

internal class DeleteUserStaffHandler(IRepository<UserStaffInfo> _repo,ISender _sender) : IRequestHandler<DeleteUserStaffRequest, int>
{
    public Task<int> Handle(DeleteUserStaffRequest request, CancellationToken cancellationToken)
        => _repo.SoftDeleteAsync(x => x.Id == request.Id, cancellationToken);
}

internal class UserStaffPageHandler(IReadRepository<UserStaffInfo> _repo) : IRequestHandler<UserStaffPageRequest, PaginationResponse<UserStaffPageOutput>>
{
    public Task<PaginationResponse<UserStaffPageOutput>> Handle(UserStaffPageRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<UserStaffInfo, bool>>? expression = request.OrgId.HasValue
          ? x => x.OrgId == request.OrgId
          : null;

        return _repo.SimplePageAsync<UserStaffPageOutput>(request, expression, cancellationToken: cancellationToken);
    }
}

internal class GetCurrentUserStaffHandler(IReadRepository<UserStaffInfo> _repo, ICurrentUser _currentUser) : IRequestHandler<GetCurrentUserStaffRequest, List<UserStaffOutput>>
{
    public async Task<List<UserStaffOutput>> Handle(GetCurrentUserStaffRequest request, CancellationToken cancellationToken)
    {
        var userStaffs = await _repo.SetGlobalFilterStatus(ignoreTenantFilter: true, ignoreDataPermissionFilter: true)
                .SimpleListAsync<UserStaffOutput>(x => x.UserId == _currentUser.UserId
                    && x.Enabled == EnabledStatusEnum.Enabled
                    && x.Org.Enabled == EnabledStatusEnum.Enabled
                    && x.Org.Tenant.Enabled == EnabledStatusEnum.Enabled, cancellationToken);

        if (userStaffs.IsNullOrEmpty())
            throw ResultOutput.Exception("非机构员工或机构与员工被停用");

        return userStaffs;
    }
}