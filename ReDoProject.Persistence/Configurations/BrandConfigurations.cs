

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReDoProject.Domain.Entities;

public class BrandConfigurations: IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        // ID - Primary Key
        builder.HasKey(i => i.Id);
        
        // Name
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        
        // DisplayingText
        builder.Property(x => x.DisplayingText).HasMaxLength(50).IsRequired();
        
        // Address
        builder.Property(x => x.Address).HasMaxLength(50).IsRequired();
        
        // SupportMail
        builder.Property(x => x.SupportMail).HasMaxLength(50).IsRequired();
        
        // SupportPhone
        builder.Property(x => x.SupportPhone).HasMaxLength(50).IsRequired();
        
        
        
        
        
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
        
        builder.ToTable("Brands");
    }
}