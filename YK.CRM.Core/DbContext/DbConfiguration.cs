using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YK.CRM.Core.Consts;
using YK.CRM.Core.Entities;

namespace YK.CRM.Core.DbContext;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("crm_customer", CrmAppConsts.DbSchema);

        builder.Property(x => x.Name).HasMaxLength(64);
    }
}
