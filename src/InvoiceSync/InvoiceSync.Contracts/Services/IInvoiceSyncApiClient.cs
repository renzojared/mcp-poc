namespace InvoiceSync.Contracts.Services;

/// <summary>
/// Interface for the Invoice Sync API client.
/// Provides methods to interact with the Invoice Sync API endpoints.
/// </summary>
public interface IInvoiceSyncApiClient
{
    /// <summary>
    /// Gets event processing information by event ID.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event (GUID format).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Event process summary containing status and delivery details.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    Task<EventProcessSummary?> GetEventByIdAsync(Guid eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets subscriptions by event type with pagination support.
    /// </summary>
    /// <param name="eventType">The type of event to filter subscriptions (e.g., "invoice.created").</param>
    /// <param name="pageNumber">Page number to retrieve (1-based). Default is 1.</param>
    /// <param name="pageSize">Number of items per page. Default is 10.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Paginated list of subscription details.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    Task<PaginatedList<SubscriptionDetail>?> GetSubscriptionsByEventTypeAsync(
        string eventType,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}