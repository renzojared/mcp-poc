namespace InvoiceSync.Contracts.Models;

public record EventProcessSummary(
    string ApplicationName,
    string RequestId,
    string EventType,
    EventStatus Status,
    IEnumerable<DeliveryTarget> DeliveryTargets
);