namespace InvoiceSync.Contracts.Models;

public enum DeliveryStatus
{
    Pending = 1,
    Success = 2,
    Failed = 3,
    Timeout = 4,
    ClauseFail = 5
}