using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ApplicationFormPractice.Domain.Entities;

namespace ApplicationFormPractice.Infrastructure.Data.Configuration;

public class CustomQuestionConfiguration : IEntityTypeConfiguration<CustomQuestion>
{
    public void Configure(EntityTypeBuilder<CustomQuestion> builder)
    {
        builder.ToContainer("CustomQuestions"); 

        builder.HasKey(c => c.Id); 

        builder.Property(c => c.Id)
               .ToJsonProperty("id");

        builder.Property(e => e.QuestionType).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Question).HasMaxLength(500).IsRequired();
    }
}
