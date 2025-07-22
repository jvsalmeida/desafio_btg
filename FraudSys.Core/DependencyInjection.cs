using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FraudSys.Core.Repositories;
using FraudSys.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FraudSys.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddLimitManagerCore(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetAWSOptions();

        services
            .AddDefaultAWSOptions(options)
            .AddAWSService<IAmazonDynamoDB>();

        services
            .AddSingleton<IDynamoDBContext, DynamoDBContext>()
            .AddScoped<ITransactionManagerRepository, TransactionManagerRepository>()
            .AddScoped<ILimitManagerRepository, LimitManagerRepository>()
            .AddScoped<ITransactionManagerService, TransactionManagerService>()
            .AddScoped<ILimitManagerService, LimitManagerService>();

        return services;
    }
}
