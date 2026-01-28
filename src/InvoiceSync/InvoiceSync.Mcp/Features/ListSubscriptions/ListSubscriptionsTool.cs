using InvoiceSync.Contracts.Services;
using InvoiceSync.Contracts.Utilities;
using ModelContextProtocol.Server;

namespace InvoiceSync.Mcp.Features.ListSubscriptions;

/// <summary>
/// MCP tool for listing subscriptions by event type.
/// </summary>
[McpServerToolType]
public class ListSubscriptionsTool(IInvoiceSyncApiClient apiClient)
{
    [McpServerTool(
        Name = "list_subscriptions",
        Title = "Lists active subscriptions for a specific event type with pagination support")]
    public async Task<ListSubscriptionsResponse> ListSubscriptionsAsync(
        string eventType,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await apiClient.GetSubscriptionsByEventTypeAsync(
            eventType, pageNumber, pageSize, cancellationToken);

        if (result is null)
            return new ListSubscriptionsResponse
            {
                EventType = eventType,
                TotalCount = 0,
                PageNumber = pageNumber,
                TotalPages = 0,
                Subscriptions = []
            };

        // Map subscriptions with masked URLs and clause counts
        var subscriptions = result.Items.Select(sub => new SubscriptionInfo
        {
            EndpointDomain = UrlMasker.GetHostOnly(sub.CallbackUrl),
            MaxRetries = sub.MaxRetries,
            TimeoutInSeconds = sub.TimeoutInSeconds,
            ClauseCount = sub.Clauses.Length
        }).ToList();

        return new ListSubscriptionsResponse
        {
            EventType = eventType,
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            TotalPages = result.TotalPages,
            HasNextPage = result.HasNextPage,
            HasPreviousPage = result.HasPreviousPage,
            Subscriptions = subscriptions
        };
    }

}

/// <summary>
/// Response model for ListSubscriptions tool.
/// </summary>
public class ListSubscriptionsResponse
{
    public string EventType { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public List<SubscriptionInfo> Subscriptions { get; set; } = [];
}

public class SubscriptionInfo
{
    public string EndpointDomain { get; set; } = string.Empty;
    public short MaxRetries { get; set; }
    public short TimeoutInSeconds { get; set; }
    public int ClauseCount { get; set; }
}