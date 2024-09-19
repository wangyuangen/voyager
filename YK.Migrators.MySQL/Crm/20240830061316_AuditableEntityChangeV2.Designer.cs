﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YK.CRM.Core.DbContext;

#nullable disable

namespace YK.Migrators.MySQL.Crm
{
    [DbContext(typeof(CrmDbContext))]
    [Migration("20240830061316_AuditableEntityChangeV2")]
    partial class AuditableEntityChangeV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("crm")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("YK.CRM.Core.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("CreatedOrgBy")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CreatedUserStaffBy")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedUserStaffName")
                        .HasColumnType("longtext");

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

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("crm_customer", "crm");
                });
#pragma warning restore 612, 618
        }
    }
}
