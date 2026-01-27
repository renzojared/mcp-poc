namespace InvoiceSync.Contracts.Models;

public record PaginatedList<T>(
    IReadOnlyCollection<T> Items,
    int PageNumber,
    int TotalPages,
    int TotalCount)
{
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}