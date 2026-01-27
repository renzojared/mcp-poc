using InvoiceSync.Contracts.Services;
using InvoiceSync.Contracts.Utilities;
using ModelContextProtocol.Server;

namespace InvoiceSync.Mcp.Features.GetEventDeliveryDetails;

/// <summary>
/// MCP tool for getting detailed delivery information for an event.
/// </summary>
public class GetEventDeliveryDetailsTool(IInvoiceSyncApiClient apiClient)
{
    [McpServerTool(
        Name = "get_event_delivery_details",
        Title =
            "Gets detailed delivery attempt information for a specific event, with masked endpoint URLs for security")]
    public async Task<GetEventDeliveryDetailsResponse> GetEventDeliveryDetailsAsync(
        Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var eventSummary = await apiClient.GetEventByIdAsync(eventId, cancellationToken);

        if (eventSummary is null)
            return new GetEventDeliveryDetailsResponse
            {
                Found = false,
                Message = $"Event with ID {eventId} not found"
            };

        // Map delivery targets with masked URLs and sanitized attempt data
        var deliveries = eventSummary.DeliveryTargets.Select(dt => new DeliveryInfo
        {
            EndpointDomain = UrlMasker.Mask(dt.Endpoint),
            LastStatus = dt.Attempts.LastOrDefault()?.Status.ToString() ?? "None",
            AttemptCount = dt.Attempts.Count(),
            LastAttemptTime = dt.Attempts.LastOrDefault()?.Timestamp
        }).ToList();

        return new GetEventDeliveryDetailsResponse
        {
            Found = true,
            EventType = eventSummary.EventType,
            Status = eventSummary.Status.ToString(),
            Deliveries = deliveries
        };
    }

}

/// <summary>
/// Response model for GetEventDeliveryDetails tool.
/// </summary>
public class GetEventDeliveryDetailsResponse
{
    public bool Found { get; set; }
    public string? Message { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<DeliveryInfo> Deliveries { get; set; } = [];
}

public class DeliveryInfo
{
    public string EndpointDomain { get; set; } = string.Empty;
    public string LastStatus { get; set; } = string.Empty;
    public int AttemptCount { get; set; }
    public DateTimeOffset? LastAttemptTime { get; set; }
}