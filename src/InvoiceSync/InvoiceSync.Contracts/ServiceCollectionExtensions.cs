using InvoiceSync.Contracts.Configuration;
using InvoiceSync.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace InvoiceSync.Contracts;

/// <summary>
/// Extension methods for configuring Invoice Sync API services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Invoice Sync API client services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddInvoiceSyncApiClient(this IServiceCollection services)
    {
        services
            .AddOptions<InvoiceSyncApiOptions>()
            .BindConfiguration(InvoiceSyncApiOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<IInvoiceSyncApiClient, InvoiceSyncApiClient>((sp, client) =>
        {
            var apiOptions = sp.GetRequiredService<IOptionsMonitor<InvoiceSyncApiOptions>>().CurrentValue;

            client.BaseAddress = new Uri(apiOptions.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(apiOptions.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("Api-Key", apiOptions.ApiKey);
        });

        return services;
    }
}