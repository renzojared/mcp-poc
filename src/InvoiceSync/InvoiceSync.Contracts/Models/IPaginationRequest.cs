namespace InvoiceSync.Contracts.Models;

public interface IPaginationRequest
{
    int PageNumber { get; }
    int PageSize { get; }
}