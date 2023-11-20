
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReDoProject.Domain.Entities;

public class InstrumentsConfiguration:IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        // ID - Primary Key
        builder.HasKey(i => i.Id);
        
        // Name
        builder.Property(i => i.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        // Description
        builder.Property(i => i.Description)
            .HasMaxLength(500)
            .IsRequired();
        
        // Price
        builder.Property(i => i.Price)
            .IsRequired();
        
        // Barcode
        builder.Property(i=> i.Barcode)
            .HasMaxLength(50)
            .IsRequired();
        
        // PictureUrl
        builder.Property(i => i.PictureUrl)
            .HasMaxLength(500)
            .IsRequired();
        
        // Type
        builder.Property(i => i.Type)
            .IsRequired();
        
        // Color
        builder.Property(i => i.Color)
            .IsRequired();
        
        
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
        
        
        // Brand
        /*builder.HasOne(i => i.Brand)
            .WithMany(b => b.Instruments)
            .HasForeignKey(i => i.BrandId)
            .IsRequired();
        */
        // Table Name
        builder.ToTable("Instruments");
        

    }
}