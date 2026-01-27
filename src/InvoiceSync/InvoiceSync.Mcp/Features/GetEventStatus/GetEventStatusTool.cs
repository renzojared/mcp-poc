using InvoiceSync.Contracts.Services;
using ModelContextProtocol.Server;

namespace InvoiceSync.Mcp.Features.GetEventStatus;

/// <summary>
/// MCP tool for getting event status information.
/// </summary>
public class GetEventStatusTool(IInvoiceSyncApiClient apiClient)
{
    [McpServerTool(
        Name = "get_event_status",
        Title = "Gets the processing status and basic information for a specific event by ID")]
    public async Task<GetEventStatusResponse> GetEventStatusAsync(
        Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var eventSummary = await apiClient.GetEventByIdAsync(eventId, cancellationToken);

        if (eventSummary is null)
            return new GetEventStatusResponse
            {
                Found = false,
                Message = $"Event with ID {eventId} not found"
            };

        // Filter response to exclude sensitive delivery details
        return new GetEventStatusResponse
        {
            Found = true,
            ApplicationName = eventSummary.ApplicationName,
            RequestId = eventSummary.RequestId,
            EventType = eventSummary.EventType,
            Status = eventSummary.Status.ToString(),
            DeliveryCount = eventSummary.DeliveryTargets.Count(),
            SuccessfulDeliveries = eventSummary.DeliveryTargets.Count(dt =>
                dt.Attempts.Any(a => a.Status == Contracts.Models.DeliveryStatus.Success)),
            FailedDeliveries = eventSummary.DeliveryTargets.Count(dt =>
                dt.Attempts.All(a => a.Status != Contracts.Models.DeliveryStatus.Success))
        };
    }
}

/// <summary>
/// Response model for GetEventStatus tool.
/// </summary>
public class GetEventStatusResponse
{
    public bool Found { get; set; }
    public string? Message { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int DeliveryCount { get; set; }
    public int SuccessfulDeliveries { get; set; }
    public int FailedDeliveries { get; set; }
}