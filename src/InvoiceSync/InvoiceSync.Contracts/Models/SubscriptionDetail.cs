namespace InvoiceSync.Contracts.Models;

public record SubscriptionDetail(
    string CallbackUrl,
    short MaxRetries,
    short TimeoutInSeconds,
    SubscriptionClause[] Clauses);