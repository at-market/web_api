using market.Extensions;
using market.Models.Common;
using market.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace market.Models.Domain;

public class Order : Entity
{
    public OrderStatus Status { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public int AddressId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = null!;
    public int? FinancialDocumentId { get; set; }
    public virtual FinancialDocument? FinancialDocument { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasSoftDelete();
        builder.HasTimestamps();

        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.AddressId).IsRequired();
        builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Pending);

        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder
            .HasOne(x => x.Address)
            .WithMany()
            .HasForeignKey(x => x.AddressId);

        builder
         .HasOne(x => x.FinancialDocument)
         .WithOne(x => x.Order)
         .HasForeignKey<Order>(x => x.FinancialDocumentId);
    }
}