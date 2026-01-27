namespace InvoiceSync.Contracts.Models;

public record DeliveryTarget(
    string Endpoint,
    IEnumerable<DeliveryAttempt> Attempts
);