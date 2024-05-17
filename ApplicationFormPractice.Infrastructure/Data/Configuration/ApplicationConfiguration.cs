using ApplicationFormPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormPractice.Infrastructure.Data.Configuration;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToContainer("Applications");

        builder.HasKey(e => e.Id); 

        builder.Property(e => e.Id)
               .ToJsonProperty("id");

        builder.Property(e => e.FirstName).HasMaxLength(20).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(20).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(11).IsRequired();
        builder.Property(e => e.Nationality).HasMaxLength(30).IsRequired();
        builder.Property(e => e.CurrentResidence).HasMaxLength(50).IsRequired();
        builder.Property(e => e.IDNumber).HasMaxLength(20).IsRequired();
        builder.Property(e => e.Gender).HasMaxLength(10).IsRequired();
    }
}
