using YK.Console.Business.Abstractors;
using YK.Console.Business.OrganizeInfos;
using YK.Console.Business.UserInfos;
using YK.Console.Business.UserStaffOrgs;
using YK.Console.Business.UserStaffRoles;

namespace YK.Console.Business.UserStaffInfos;

public class UserStaffOperation(ISender _sender,IReadRepository<UserInfo> _userRepo,IRepository<UserStaffInfo> _repo) : IUserStaffOperation
{
    public async Task<Guid> CreateAsync(CreateUserStaffRequest request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Empty;
        var user = await _userRepo.SimpleSingleAsync<UserInfoOutput>(x => x.Mobile == request.UserInfo.Mobile, cancellationToken);
        if (user.IsNull())
        {
            var createUserReq = request.UserInfo.Adapt<CreateUserInfoRequest>();
            userId = await _sender.Send(createUserReq, cancellationToken);
        }
        else
        {
            userId = user?.Id ?? Guid.Empty;
            var updateUserReq = request.UserInfo.Adapt<UpdateUserInfoRequest>();
            updateUserReq.Id = userId;
            await _sender.Send(updateUserReq, cancellationToken);
        }

        //创建普通员工
        var userStaff = request.Adapt<UserStaffInfo>();
        userStaff.UserId = userId;
        userStaff.UserStaffType = UserStaffTypeEnum.Normal;
        await _repo.AddAsync(userStaff, cancellationToken);

        //员工角色
        await _sender.Send(new SaveUserStaffRoleRequest
        {
            RoleIds = request.RoleIds,
            UserStaffId = userStaff.Id
        }, cancellationToken);

        //员工附属部门
        await _sender.Send(new SaveUserStaffOrgRequest
        {
            OrgIds = request.OrgIds ?? new List<Guid>(),
            UserStaffId = userStaff.Id
        }, cancellationToken);

        return userStaff.Id;
    }

    public async Task<Guid> UpdateAsync(UpdateUserStaffRequest request, CancellationToken cancellationToken)
    {
        //保存员工
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        _ = entity ?? throw ResultOutput.Exception("员工不存在");

        entity.Update(null, request.RealName, request.PostId, request.JobNo, request.Enabled, request.EntryDate, request.Remark, request.OrgId, request.IsManager, null);
        await _repo.UpdateAsync(entity, cancellationToken);

        //员工角色
        await _sender.Send(new SaveUserStaffRoleRequest
        {
            RoleIds = request.RoleIds,
            UserStaffId = entity.Id
        }, cancellationToken);

        //员工附属部门
        await _sender.Send(new SaveUserStaffOrgRequest
        {
            OrgIds = request.OrgIds ?? new List<Guid>(),
            UserStaffId = entity.Id
        }, cancellationToken);

        return entity.Id;
    }
}
