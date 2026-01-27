namespace InvoiceSync.Contracts.Models;

public enum EventStatus
{
    Pending = 1,
    Processing = 2,
    Completed = 3,
    CompletedNoSubscribers = 4
}