using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReDoProject.Domain.Entities;

namespace ReDoProject.Persistence.Configurations;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        
        // ID - Primary Key
        builder.HasKey(i => i.Id);
        
        // IsDelivered
        builder.Property(i => i.IsDelivered)
            .IsRequired().HasDefaultValue("false");
        
        // Ordered Basket
        
        /*builder.HasOne(i => i.OrderedBasket)
            .WithOne(i => i.OrderedOrder)
            .HasForeignKey<Order>(i => i.Id);
        */
        // COMMON PROPERTIES
        
        // DeletedOn
        builder.Property(i => i.DeletedOn)
            .IsRequired(false);
        
        // CreatedOn
        builder.Property(i => i.CreatedOn)
            .IsRequired();
        
        // isDeleted
        builder.Property(i => i.IsDeleted)
            .IsRequired();
        
        // ModifiedOn
        builder.Property(i => i.ModifiedOn)
            .IsRequired();
        
        // CreatedByUserId
        builder.Property(i => i.CreatedByUserId)
            .HasMaxLength(50)
            .IsRequired();
        
        // ModifiedByUserId
        builder.Property(i => i.ModifiedByUserId)
            .HasMaxLength(50)
            .IsRequired();
        
        // DeletedByUserId
        builder.Property(i => i.DeletedByUserId)
            .HasMaxLength(50)
            .IsRequired();
        
        // To Table
        
        builder.ToTable("Orders");
    }
}