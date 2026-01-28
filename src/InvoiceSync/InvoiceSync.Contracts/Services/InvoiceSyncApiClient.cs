using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoiceSync.Contracts.Services;

/// <summary>
/// HTTP client implementation for the Invoice Sync API.
/// </summary>
internal sealed class InvoiceSyncApiClient(HttpClient httpClient) : IInvoiceSyncApiClient
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    /// <inheritdoc />
    public async Task<EventProcessSummary?> GetEventByIdAsync(Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/v1/event/{eventId}", cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<EventProcessSummary>(_jsonOptions, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PaginatedList<SubscriptionDetail>?> GetSubscriptionsByEventTypeAsync(
        string eventType,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventType);

        var url = $"api/v1/subscription/{Uri.EscapeDataString(eventType)}?PageNumber={pageNumber}&PageSize={pageSize}";
        var response = await httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PaginatedList<SubscriptionDetail>>(_jsonOptions,
            cancellationToken);
    }
}