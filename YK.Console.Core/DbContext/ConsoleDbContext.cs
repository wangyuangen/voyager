using YK.Console.Core.Consts;
using YK.Console.Core.Entities;

namespace YK.Console.Core.DbContext; 

/// <summary>
/// console数据库上下文
/// </summary>
public sealed class ConsoleDbContext(
    DbContextOptions<ConsoleDbContext> options,
     IEventPublisher publisher,
     ICurrentUser currentUser) : BaseDbContext(options, publisher,currentUser)
{
    public DbSet<ApiInfo> ApiInfos { get; set; }
    public DbSet<MenuRouteInfo> MenuRouteInfos { get; set; }
    public DbSet<OrganizeInfo> OrganizeInfos { get; set; }
    public DbSet<PackageInfo> PackageInfos { get; set; }
    public DbSet<PackageMenuRoute> PackageMenuRoutes { get; set; }
    public DbSet<PackagePermissionGroup> PackagePermissionGroups { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    public DbSet<PermissionGroupApi> PermissionGroupApis { get; set; }
    public DbSet<PostInfo> PostInfos { get; set; }
    public DbSet<RoleInfo> RoleInfos { get; set; }
    public DbSet<RoleMenuRoute> RoleMenuRoutes { get; set; }
    public DbSet<RolePermissionGroup> RolePermissionGroups { get; set; }
    public DbSet<TenantInfo> TenantInfos { get; set; }
    public DbSet<TenantPackage> TenantPackages { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<UserStaffInfo> UserStaffInfos { get; set; }
    public DbSet<UserStaffOrg> UserStaffOrgs { get; set; }
    public DbSet<UserStaffRole> UserStaffRoles { get; set; }
    public DbSet<ViewInfo> ViewInfos { get; set; }
    public DbSet<DataDictInfo> DataDicts { get; set; }
    public DbSet<FileStorageInfo> FileStorageInfos { get; set; }
    public DbSet<RegionInfo> RegionInfos { get; set; }

    /// <summary>
    /// 模型创建
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsoleDbContext).Assembly);
        modelBuilder.HasDefaultSchema(ConsoleAppConsts.DbSchema);
        AppendGlobalFilters(modelBuilder);
    }
}
