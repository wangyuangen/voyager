﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YK.Console.Core.DbContext;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    [DbContext(typeof(ConsoleDbContext))]
    [Migration("20240820093018_UserStaffRealName")]
    partial class UserStaffRealName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("console")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("YK.Console.Core.Entities.ApiInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<string>("HttpMethod")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Path")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cs_api_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.MenuRouteInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<bool>("External")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Hidden")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Icon")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("IsAffix")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsIframe")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsKeepAlive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Link")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("NewWindow")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Opened")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RedirectUrl")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("RouteName")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("RouteUrl")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("ViewId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ViewInfoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ViewInfoId");

                    b.ToTable("cs_menu_route_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.OrganizeInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeCount")
                        .HasColumnType("int");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("OrganizeType")
                        .HasColumnType("int");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("cs_organize_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PackageInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cs_package_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PackageMenuRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MenuRouteId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MenuRouteId");

                    b.ToTable("cs_package_menu_route", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PackagePermissionGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PermissionGroupId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionGroupId");

                    b.ToTable("cs_package_permission_group", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PermissionGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("Scope")
                        .HasColumnType("int");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cs_permission_group", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PermissionGroupApi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ApiId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PermissionGroupId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ApiId");

                    b.ToTable("cs_permission_group_api", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PostInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("cs_post_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.RoleInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("cs_role_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.RoleMenuRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MenuRouteId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MenuRouteId");

                    b.ToTable("cs_role_menu_route", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.RolePermissionGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PermissionGroupId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionGroupId");

                    b.ToTable("cs_role_permission_group", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.TenantInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("ContactMobile")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("TenantType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cs_tenant_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.TenantPackage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("TenantId");

                    b.ToTable("cs_tenant_package", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<Guid?>("AvatarFileId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("PasswordEncryptType")
                        .HasColumnType("int");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cs_user_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserStaffInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsManager")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("JobNo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OrgId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrgId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("cs_user_staff_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserStaffOrg", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OrgId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserStaffId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrgId");

                    b.ToTable("cs_user_staff_org", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserStaffRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserStaffId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("cs_user_staff_role", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.ViewInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<bool>("IsKeepAlive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cs_view_info", "console");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.MenuRouteInfo", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.ViewInfo", "ViewInfo")
                        .WithMany("MenuRouteInfos")
                        .HasForeignKey("ViewInfoId");

                    b.Navigation("ViewInfo");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.OrganizeInfo", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.TenantInfo", "Tenant")
                        .WithMany("OrganizeInfos")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PackageMenuRoute", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.MenuRouteInfo", "MenuRoute")
                        .WithMany("PackageMenuRoutes")
                        .HasForeignKey("MenuRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuRoute");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PackagePermissionGroup", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.PermissionGroup", "PermissionGroup")
                        .WithMany("PackagePermissionGroups")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionGroup");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PermissionGroupApi", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.ApiInfo", "Api")
                        .WithMany("PermissionGroupApis")
                        .HasForeignKey("ApiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Api");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.RoleMenuRoute", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.MenuRouteInfo", "MenuRoute")
                        .WithMany("RoleMenuRoutes")
                        .HasForeignKey("MenuRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuRoute");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.RolePermissionGroup", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.PermissionGroup", "PermissionGroup")
                        .WithMany("RolePermissionGroups")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionGroup");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.TenantPackage", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.PackageInfo", "Package")
                        .WithMany("TenantPackages")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YK.Console.Core.Entities.TenantInfo", "Tenant")
                        .WithMany("TenantPackages")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserStaffInfo", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.OrganizeInfo", "Org")
                        .WithMany("UserStaffInfos")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YK.Console.Core.Entities.PostInfo", "Post")
                        .WithMany("UserStaffInfos")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YK.Console.Core.Entities.UserInfo", "User")
                        .WithMany("UserStaffInfos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Org");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserStaffOrg", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.OrganizeInfo", "Org")
                        .WithMany("UserStaffOrgs")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Org");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserStaffRole", b =>
                {
                    b.HasOne("YK.Console.Core.Entities.RoleInfo", "Role")
                        .WithMany("UserStaffRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.ApiInfo", b =>
                {
                    b.Navigation("PermissionGroupApis");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.MenuRouteInfo", b =>
                {
                    b.Navigation("PackageMenuRoutes");

                    b.Navigation("RoleMenuRoutes");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.OrganizeInfo", b =>
                {
                    b.Navigation("UserStaffInfos");

                    b.Navigation("UserStaffOrgs");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PackageInfo", b =>
                {
                    b.Navigation("TenantPackages");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PermissionGroup", b =>
                {
                    b.Navigation("PackagePermissionGroups");

                    b.Navigation("RolePermissionGroups");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.PostInfo", b =>
                {
                    b.Navigation("UserStaffInfos");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.RoleInfo", b =>
                {
                    b.Navigation("UserStaffRoles");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.TenantInfo", b =>
                {
                    b.Navigation("OrganizeInfos");

                    b.Navigation("TenantPackages");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.UserInfo", b =>
                {
                    b.Navigation("UserStaffInfos");
                });

            modelBuilder.Entity("YK.Console.Core.Entities.ViewInfo", b =>
                {
                    b.Navigation("MenuRouteInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
