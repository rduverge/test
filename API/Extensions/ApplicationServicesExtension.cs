
using CuentasPorCobrar.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;
using static System.Console;


namespace API.Extensions;

public static class ApplicationServicesExtension
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Add services to the container.
        services.AddCors(opt => {
            opt.AddPolicy("CorsPolicy", policy => {
                policy.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://127.0.0.1:5173");
            });
        });

        services.AddControllers().
            AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                
            }).AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
       

    ); 
       

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        // services.AddSwaggerGen();
        services.AddCuentasContext();

        services.AddControllers(options =>
        {
            WriteLine("Default ouput formatters:");
            foreach (IOutputFormatter formatter in options.OutputFormatters)
            {
                OutputFormatter? mediaFormatter = formatter as OutputFormatter;
                if (mediaFormatter is null)
                {
                    WriteLine($" {formatter.GetType().Name}");
                }
                else //OutputFormatter class has SupportedMediaTypes
                {
                    WriteLine(" {0}, Media types: {1}",
                        mediaFormatter.GetType().Name,
                        string.Join(", ",
                        mediaFormatter.SupportedMediaTypes));

                }
            }
        })
            .AddXmlDataContractSerializerFormatters()
            .AddXmlSerializerFormatters();

        //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<WeatherForecastValidator>());


        services.AddEndpointsApiExplorer();


        services.AddSwaggerGen(doc =>
        {
            doc.SwaggerDoc("v1", new()
            {
                Title="Cuentas Por Cobrar Service API",
                Version="v1"
            });


        });
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IAccountingEntryRepository, AccountingEntryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IValidator<Document>, DocumentValidator>();
        services.AddScoped<IValidator<AccountingEntry>, AccountingEntriesValidator>();

        //builder.Services.AddScoped<ValidationFilterAttribute>();

        //builder.Services.Configure<ApiBehaviorOptions>(options =>
        //options.SuppressModelStateInvalidFilter=true); 
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
        services.AddScoped<IValidator<Transaction>, TransactionValidator>();

        return services;

    }
    
}

