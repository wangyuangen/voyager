using YK.BackgroundJob.Abstractions;
using YK.Console.Business.Abstractors;
using YK.Console.Business.TenantPackages;

namespace YK.Console.Business.Tenants;

internal class TenantPageHandler(IReadRepository<TenantInfo> _repo) : IRequestHandler<TenantPageRequest, PaginationResponse<TenantOutput>>
{
    public Task<PaginationResponse<TenantOutput>> Handle(TenantPageRequest request, CancellationToken cancellationToken)
        => _repo.SimplePageAsync<TenantOutput>(request, cancellationToken: cancellationToken);
}

internal class CreateTenantHandler(IRepository<TenantInfo> _repo, ISender _sender,IBackgroundJob _job) : IRequestHandler<CreateTenantRequest, Guid>
{
    public async Task<Guid> Handle(CreateTenantRequest request, CancellationToken cancellationToken)
    {
        var tenant = request.Adapt<TenantInfo>();

        //创建普通租户
        tenant.TenantType = TenantTypeEnum.NormalTenant;
        await _repo.AddAsync(tenant, cancellationToken);
        
        //关联租户套餐
        await _sender.Send(new SaveTenantPackageRequest
        {
            TenantId = tenant.Id,
            PackageIds = request.PackageIds
        }, cancellationToken);

        if(tenant.Enabled == EnabledStatusEnum.Enabled)
        {
            //租户初始化
            _job.Enqueue<ITenantJob>(x => x.InitialAsync(tenant, cancellationToken));
        }

        return tenant.Id;
    }
}

internal class UpdateTenantHandler(IRepository<TenantInfo> _repo, ISender _sender, IBackgroundJob _job) : IRequestHandler<UpdateTenantRequest, Guid>
{
    public async Task<Guid> Handle(UpdateTenantRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _repo.GetByIdAsync(request.Id, cancellationToken);
        _ = tenant ?? throw ResultOutput.Exception("租户不存在");

        var oldEnabledStatus = tenant.Enabled;
        tenant.Update(request.Name, request.CompanyName, request.ContactPerson, request.ContactMobile, request.Remark, null,
            request.Enabled, request.ExpiryDate,request.RegionCode,request.RegionText);
        await _repo.UpdateAsync(tenant, cancellationToken);

        //关联租户套餐
        await _sender.Send(new SaveTenantPackageRequest
        {
            TenantId = tenant.Id,
            PackageIds = request.PackageIds
        }, cancellationToken);

        if (oldEnabledStatus != tenant.Enabled && tenant.Enabled == EnabledStatusEnum.Enabled)
        {
            //租户初始化
            _job.Enqueue<ITenantJob>(x => x.InitialAsync(tenant, cancellationToken));
        }

        return tenant.Id;
    }
}
