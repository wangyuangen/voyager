using Microsoft.EntityFrameworkCore;
using YK.Console.Business.Abstractors;
using YK.Console.Core.DbContext;

namespace YK.Console.Business.Tenants;

public class TenantJob(ConsoleDbContext _dbContext) : ITenantJob
{
    public async Task InitialAsync(TenantInfo tenantInfo, CancellationToken cancellationToken = default)
    {
        //通过租户机构是否存在判断是否有过初始化
        if (await _dbContext.Set<OrganizeInfo>().AsNoTracking().AnyAsync(x => x.TenantId == tenantInfo.Id, cancellationToken))
            return;

        var appOptions = AppCore.GetConfig<AppOptions>();

        //创建机构
        var org = new OrganizeInfo
        {
            Enabled = EnabledStatusEnum.Enabled,
            Name = tenantInfo.CompanyName,
            OrganizeType = OrganizeTypeEnum.Company,
            ParentId = Guid.Empty,
            Remark = "租户初始化创建的默认机构",
            Sort = 1,
            TenantId = tenantInfo.Id,
            RegionCode = tenantInfo.RegionCode,
            RegionText = tenantInfo.RegionText,
        };

        await _dbContext.Set<OrganizeInfo>().AddAsync(org, cancellationToken);

        //创建岗位
        var post = new PostInfo
        {
            TenantId = tenantInfo.Id,
            Name = "管理员",
            Remark = "租户初始化创建的默认岗位",
        };

        await _dbContext.Set<PostInfo>().AddAsync(post, cancellationToken);

        //创建用户
        var user = await _dbContext.Set<UserInfo>().AsNoTracking().FirstOrDefaultAsync(x => x.Mobile == tenantInfo.ContactMobile, cancellationToken);
        if (user.IsNull())
        {
            user = new UserInfo
            {
                Account = tenantInfo.ContactMobile,
                Mobile = tenantInfo.ContactMobile,
                NickName = tenantInfo.Name,
                PasswordEncryptType = appOptions.PasswordEncryptType
            };
            user.EncryptPassword(appOptions.DefaultPassword);

            await _dbContext.Set<UserInfo>().AddAsync(user, cancellationToken);
        }

        //获取租户套餐
        var tenantPackages = await _dbContext.Set<TenantPackage>()
            .AsNoTracking()
            .Where(x => x.TenantId == tenantInfo.Id)
            .Include(x => x.Package.PackageMenuRoutes)
            .Include(x => x.Package.PackagePermissionGroups)
            .ToListAsync(cancellationToken);

        //创建角色
        var role = new RoleInfo
        {
            Enabled = EnabledStatusEnum.Enabled,
            Name = "管理员",
            Remark = "租户初始化创建的默认角色",
            Sort = 1,
            TenantId = tenantInfo.Id
        };

        await _dbContext.Set<RoleInfo>().AddAsync(role, cancellationToken);

        var roleMenuRoutes = tenantPackages.SelectMany(x => x.Package.PackageMenuRoutes.Select(m => new RoleMenuRoute
        {
            MenuRouteId = m.MenuRouteId,
            RoleId = role.Id
        }));

        await _dbContext.Set<RoleMenuRoute>().AddRangeAsync(roleMenuRoutes, cancellationToken);

        var rolePermissionGroups = tenantPackages.SelectMany(x => x.Package.PackagePermissionGroups.Select(p => new RolePermissionGroup
        {
            RoleId = role.Id,
            PermissionGroupId = p.PermissionGroupId
        }));

        await _dbContext.Set<RolePermissionGroup>().AddRangeAsync(rolePermissionGroups, cancellationToken);

        //创建租户管理员员工
        var userStaff = new UserStaffInfo
        {
            Enabled = EnabledStatusEnum.Enabled,
            TenantId = tenantInfo.Id,
            EntryDate = DateTime.Now,
            IsManager = false,
            JobNo = "1000001",
            OrgId = org.Id,
            PostId = post.Id,
            RealName = tenantInfo.ContactPerson,
            Remark = "租户初始化创建的默认员工",
            UserId = user?.Id ?? Guid.Empty,
            UserStaffType = UserStaffTypeEnum.TenantAdmin
        };

        await _dbContext.Set<UserStaffInfo>().AddAsync(userStaff, cancellationToken);

        var userStaffRole = new UserStaffRole
        {
            RoleId = role.Id,
            UserStaffId = userStaff.Id,
        };

        await _dbContext.Set<UserStaffRole>().AddAsync(userStaffRole, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
