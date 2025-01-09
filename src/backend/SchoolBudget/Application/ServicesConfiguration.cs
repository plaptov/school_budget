using Microsoft.EntityFrameworkCore;
using SchoolBudget.Dal;
using SchoolBudget.Interfaces;
using SchoolBudget.Services;

namespace SchoolBudget.Application;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) => services
        .AddDbContext<SchoolDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")))
        .AddScoped(typeof(IRepository<>), typeof(BaseRepository<>))
        .AddScoped<IFundsRepository, FundsRepository>()
        .AddScoped<IStudentService, StudentsService>()
        .AddScoped<IFundraisingsService, FundraisingsService>()
        ;
}
