using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Infrastructure.Data.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormPractice.Infrastructure.Data;
public class ApplicationFormDbContext : DbContext
{
    public ApplicationFormDbContext(DbContextOptions<ApplicationFormDbContext> options) : base(options)
    {

    }

    public DbSet<CustomQuestion> CustomQuestions { get; set; }
    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomQuestionConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
    }
}
