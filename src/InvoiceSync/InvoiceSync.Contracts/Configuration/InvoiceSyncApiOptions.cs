namespace InvoiceSync.Contracts.Configuration;

/// <summary>
/// Configuration options for the Invoice Sync API client.
/// </summary>
public class InvoiceSyncApiOptions
{
    /// <summary>
    /// Configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "InvoiceSyncApi";

    /// <summary>
    /// Base URL of the Invoice Sync API (e.g., "https://api.yourcompany.com").
    /// </summary>
    [Required(ErrorMessage = "Base URL is required")]
    [Url(ErrorMessage = "Base URL must be a valid URL")]
    public string BaseUrl { get; set; } = null!;

    /// <summary>
    /// API key for authentication via Api-Key header.
    /// </summary>
    [Required(ErrorMessage = "API Key is required")]
    [MinLength(1, ErrorMessage = "API Key cannot be empty")]
    public string ApiKey { get; set; } = null!;

    /// <summary>
    /// HTTP request timeout in seconds. Default is 30 seconds.
    /// </summary>
    [Range(1, 300, ErrorMessage = "Timeout must be between 1 and 300 seconds")]
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Maximum number of retry attempts for failed requests. Default is 3.
    /// </summary>
    [Range(0, 10, ErrorMessage = "Max retries must be between 0 and 10")]
    public int MaxRetries { get; set; } = 3;
}