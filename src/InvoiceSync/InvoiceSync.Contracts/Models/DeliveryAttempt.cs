namespace InvoiceSync.Contracts.Models;

public record DeliveryAttempt(
    DeliveryStatus Status,
    short StatusCode,
    DateTimeOffset Timestamp,
    IReadOnlyCollection<SubscriptionClause>? UnfulfilledClauses = null
);