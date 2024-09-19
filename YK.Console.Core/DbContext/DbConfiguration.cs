using YK.Console.Core.Consts;
using YK.Console.Core.Entities;

namespace YK.Console.Core.DbContext;

internal sealed class RegionInfoConfiguration : IEntityTypeConfiguration<RegionInfo>
{
    public void Configure(EntityTypeBuilder<RegionInfo> builder)
    {
        builder.ToTable("cs_region_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Code).HasMaxLength(32);
        builder.Property(x => x.ParentCode).HasMaxLength(32);
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Pinyin).HasMaxLength(128);
        builder.Property(x => x.PinyinFirst).HasMaxLength(64);
        builder.Property(x => x.Url).HasMaxLength(128);
        builder.Property(x => x.VilageCode).HasMaxLength(32);
    }
}

internal sealed class FileStorageInfoConfiguration : IEntityTypeConfiguration<FileStorageInfo>
{
    public void Configure(EntityTypeBuilder<FileStorageInfo> builder)
    {
        builder.ToTable("cs_file_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.BucketName).HasMaxLength(64);
        builder.Property(x => x.FileDirectory).HasMaxLength(512);
        builder.Property(x => x.SaveFileName).HasMaxLength(256);
        builder.Property(x => x.FileName).HasMaxLength(256);
        builder.Property(x => x.Extension).HasMaxLength(16);
        builder.Property(x => x.SizeFormat).HasMaxLength(64);
        builder.Property(x => x.LinkUrl).HasMaxLength(1024);
        builder.Property(x => x.BizName).HasMaxLength(64);
    }
}

internal sealed class DataDictInfoConfiguration : IEntityTypeConfiguration<DataDictInfo>
{
    public void Configure(EntityTypeBuilder<DataDictInfo> builder)
    {
        builder.ToTable("cs_data_dict", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Code).HasMaxLength(32);
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Remark).HasMaxLength(512);
        builder.Property(x => x.ParentCode).HasMaxLength(32);
        builder.Property(x => x.ThemeStyle).HasMaxLength(64);
    }
}

internal sealed class ApiInfoConfiguration : IEntityTypeConfiguration<ApiInfo>
{
    public void Configure(EntityTypeBuilder<ApiInfo> builder)
    {
        builder.ToTable("cs_api_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.HttpMethod).HasMaxLength(16);
        builder.Property(x=>x.Path).HasMaxLength(128);
        builder.Property(x => x.Remark).HasMaxLength(512);
        builder.Property(x => x.Code).HasMaxLength(128);
    }
}

internal sealed class MenuRouteInfoConfiguration : IEntityTypeConfiguration<MenuRouteInfo>
{
    public void Configure(EntityTypeBuilder<MenuRouteInfo> builder)
    {
        builder.ToTable("cs_menu_route_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.RouteUrl).HasMaxLength(128);
        builder.Property(x => x.RouteName).HasMaxLength(64);
        builder.Property(x => x.RedirectUrl).HasMaxLength(128);
        builder.Property(x => x.Icon).HasMaxLength(64);
        builder.Property(x => x.Link).HasMaxLength(512);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}

internal sealed class OrganizeInfoConfiguration : IEntityTypeConfiguration<OrganizeInfo>
{
    public void Configure(EntityTypeBuilder<OrganizeInfo> builder)
    {
        builder.ToTable("cs_organize_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Remark).HasMaxLength(512);
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.RegionCode).HasMaxLength(32);
        builder.Property(x => x.RegionText).HasMaxLength(256);
    }
}

internal sealed class PackageInfoConfiguration : IEntityTypeConfiguration<PackageInfo>
{
    public void Configure(EntityTypeBuilder<PackageInfo> builder)
    {
        builder.ToTable("cs_package_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}

internal sealed class PackageMenuRouteConfiguration : IEntityTypeConfiguration<PackageMenuRoute>
{
    public void Configure(EntityTypeBuilder<PackageMenuRoute> builder)
    {
        builder.ToTable("cs_package_menu_route", ConsoleAppConsts.DbSchema);
    }
}

internal sealed class PackagePermissionGroupConfiguration : IEntityTypeConfiguration<PackagePermissionGroup>
{
    public void Configure(EntityTypeBuilder<PackagePermissionGroup> builder)
    {
        builder.ToTable("cs_package_permission_group", ConsoleAppConsts.DbSchema);
    }
}

internal sealed class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
{
    public void Configure(EntityTypeBuilder<PermissionGroup> builder)
    {
        builder.ToTable("cs_permission_group", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}

internal sealed class PermissionGroupApiConfiguration : IEntityTypeConfiguration<PermissionGroupApi>
{
    public void Configure(EntityTypeBuilder<PermissionGroupApi> builder)
    {
        builder.ToTable("cs_permission_group_api", ConsoleAppConsts.DbSchema);
    }
}


internal sealed class PostInfoConfiguration : IEntityTypeConfiguration<PostInfo>
{
    public void Configure(EntityTypeBuilder<PostInfo> builder)
    {
        builder.ToTable("cs_post_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}

internal sealed class RoleInfoConfiguration : IEntityTypeConfiguration<RoleInfo>
{
    public void Configure(EntityTypeBuilder<RoleInfo> builder)
    {
        builder.ToTable("cs_role_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}

internal sealed class RoleMenuRouteConfiguration : IEntityTypeConfiguration<RoleMenuRoute>
{
    public void Configure(EntityTypeBuilder<RoleMenuRoute> builder)
    {
        builder.ToTable("cs_role_menu_route", ConsoleAppConsts.DbSchema);
    }
}

internal sealed class RolePermissionGroupConfiguration : IEntityTypeConfiguration<RolePermissionGroup>
{
    public void Configure(EntityTypeBuilder<RolePermissionGroup> builder)
    {
        builder.ToTable("cs_role_permission_group", ConsoleAppConsts.DbSchema);
    }
}

internal sealed class TenantInfoConfiguration : IEntityTypeConfiguration<TenantInfo>
{
    public void Configure(EntityTypeBuilder<TenantInfo> builder)
    {
        builder.ToTable("cs_tenant_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Remark).HasMaxLength(512);
        builder.Property(x => x.CompanyName).HasMaxLength(64);
        builder.Property(x => x.ContactMobile).HasMaxLength(16);
        builder.Property(x => x.ContactPerson).HasMaxLength(16);
        builder.Property(x => x.RegionCode).HasMaxLength(32);
        builder.Property(x => x.RegionText).HasMaxLength(256);
    }
}

internal sealed class TenantPackageConfiguration : IEntityTypeConfiguration<TenantPackage>
{
    public void Configure(EntityTypeBuilder<TenantPackage> builder)
    {
        builder.ToTable("cs_tenant_package", ConsoleAppConsts.DbSchema);
    }
}

/// <summary>
/// 用户表属性映射
/// </summary>
internal sealed class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("cs_user_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Account).HasMaxLength(16);
        builder.Property(x => x.NickName).HasMaxLength(16);
        builder.Property(x => x.Email).HasMaxLength(64);
        builder.Property(x => x.Mobile).HasMaxLength(16);
        builder.Property(x => x.Password).HasMaxLength(256);
        builder.Property(x => x.AvatarUrl).HasMaxLength(1024);
    }
}

internal sealed class UserStaffInfoConfiguration : IEntityTypeConfiguration<UserStaffInfo>
{
    public void Configure(EntityTypeBuilder<UserStaffInfo> builder)
    {
        builder.ToTable("cs_user_staff_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.JobNo).HasMaxLength(32);
        builder.Property(x => x.RealName).HasMaxLength(16);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}

internal sealed class UserStaffOrgConfiguration : IEntityTypeConfiguration<UserStaffOrg>
{
    public void Configure(EntityTypeBuilder<UserStaffOrg> builder)
    {
        builder.ToTable("cs_user_staff_org", ConsoleAppConsts.DbSchema);
    }
}

internal sealed class UserStaffRoleConfiguration : IEntityTypeConfiguration<UserStaffRole>
{
    public void Configure(EntityTypeBuilder<UserStaffRole> builder)
    {
        builder.ToTable("cs_user_staff_role", ConsoleAppConsts.DbSchema);   
    }
}

internal sealed class ViewInfoConfiguration : IEntityTypeConfiguration<ViewInfo>
{
    public void Configure(EntityTypeBuilder<ViewInfo> builder)
    {
        builder.ToTable("cs_view_info", ConsoleAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.RouteName).HasMaxLength(64);
        builder.Property(x => x.Path).HasMaxLength(128);
        builder.Property(x => x.Remark).HasMaxLength(512);
    }
}


