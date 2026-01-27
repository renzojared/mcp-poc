namespace InvoiceSync.Contracts.Models;

public record SubscriptionClause(string Path, ClauseOperator Operator, object? Value, bool Required);