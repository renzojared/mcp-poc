namespace InvoiceSync.Contracts.Utilities;

/// <summary>
/// Utilities for masking sensitive URL information.
/// </summary>
public static class UrlMasker
{
    /// <summary>
    /// Masks a URL by keeping only the scheme and host, replacing the rest with asterisks.
    /// Example: https://api.example.com/v1/webhook -> https://api.example.com/***
    /// </summary>
    /// <param name="url">The URL to mask.</param>
    /// <returns>The masked URL string.</returns>
    public static string Mask(string? url)
    {
        if (string.IsNullOrWhiteSpace(url) || url.StartsWith('/')) return "***";

        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            ? $"{uri.Scheme}://{uri.Host}/***"
            : "***";
    }

    /// <summary>
    /// Masks a URL by keeping only the host.
    /// Example: https://api.example.com/v1/webhook -> api.example.com
    /// </summary>
    /// <param name="url">The URL to mask.</param>
    /// <returns>The host string.</returns>
    public static string GetHostOnly(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return "***";

        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            ? uri.Host
            : "***";
    }
}