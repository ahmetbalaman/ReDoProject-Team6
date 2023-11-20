using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReDoProject.Domain.Entities;

namespace ReDoProject.Persistence.Configurations;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    
  
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        
        // ID - Primary Key
        builder.HasKey(i => i.Id);

        // Name
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        
        // Email
        builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
        
        // Address
        
        builder.Property(x => x.Address).HasMaxLength(50).IsRequired();
        
        // Phone
        builder.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired();
        
        // Role
        builder.Property(x => x.Role).IsRequired().HasDefaultValue("Customer");
        
        // Password
        builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
        
        
        
        
        
        
        
        
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
        
        builder.ToTable("Customer");

    }
}