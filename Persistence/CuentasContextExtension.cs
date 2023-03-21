using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CuentasPorCobrar.Shared; 

public static class CuentasContextExtension
{
    /// <summary>
    /// Adds CuentasDbContext to the specified 
    /// IServiceCollection. Uses the Postgres database
    /// provider
    /// </summary>
    /// <param name="services"></param>
    /// <param name="relativePath">
    /// Set to override the default of ".."
    /// </param>
    /// <returns> An IServiceCollection that can be used to add more services</returns>
    /// 

    public static IServiceCollection AddCuentasContext(this IServiceCollection services,
        string connectionString= "Host=localhost;Port=5432;Database=cuentasporcobrardb;Username=postgres;Password=Euren002")
    {
        services.AddDbContext<CuentasporcobrardbContext>(options => {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            options.UseNpgsql(connectionString);
          //  options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        return services; 
    }
}

