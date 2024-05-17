using ApplicationFormPractice.Application.Contract;
using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.RepositoryContracts;
using ApplicationFormPractice.Infrastructure.Data;
using ApplicationFormPractice.Infrastructure.Repository;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormPractice.API.Extensions;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cosmosDbConnectionString = configuration.GetConnectionString("ApplicationFormDbContext");

        services.AddDbContext<ApplicationFormDbContext>(options =>
        {
            options.UseCosmos(
                cosmosDbConnectionString,
                databaseName: "ApplicationFormDb");
        });
    }

    public static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationFormRepository, ApplicationFormRepository>();
        services.AddScoped<IApplicationFormService, ApplicationFormService>();
    }
}
