# Invoice Sync MCP

Invoice Sync API integration: Event Status, Delivery Details, Subscriptions.

**Location:** `src/InvoiceSync/` | **Image:** `mcp-invoice-sync` | **API Spec:** `docs/invoice-sync-swagger.json`

## Environment Variables

| Variable                  | Example                     |
|---------------------------|-----------------------------|
| `InvoiceSyncApi__BaseUrl` | `https://your-api-url.com`  |
| `InvoiceSyncApi__ApiKey`  | `your-invoice-sync-api-key` |

## Usage

```bash
# Build
# docker compose build --no-cache
docker build --no-cache -t mcp-invoice-sync -f src/InvoiceSync/InvoiceSync.Mcp/Dockerfile .

# Verify (optional - server will exit immediately, this is normal)
docker run --rm \
  -e InvoiceSyncApi__BaseUrl="https://your-api-url.com" \
  -e InvoiceSyncApi__ApiKey="your-invoice-sync-api-key" \
  mcp-invoice-sync
# Look for "Application started" without errors

# Test with Inspector
npx @modelcontextprotocol/inspector \
  -e InvoiceSyncApi__BaseUrl="https://your-api-url.com" \
  -e InvoiceSyncApi__ApiKey="your-invoice-sync-api-key" \
  -- docker run -i --rm -e InvoiceSyncApi__BaseUrl -e InvoiceSyncApi__ApiKey mcp-invoice-sync
```

## Client Configuration

Configure in your MCP client's configuration file. Example:

```json
{
  "mcpServers": {
    "invoiceSync": {
      "command": "docker",
      "args": ["run", "-i", "--rm", "-e", "InvoiceSyncApi__BaseUrl", "-e", "InvoiceSyncApi__ApiKey", "mcp-invoice-sync"],
      "env": {
        "InvoiceSyncApi__BaseUrl": "https://your-api-url.com",
        "InvoiceSyncApi__ApiKey": "your-invoice-sync-api-key"
      }
    }
  }
}
```

Refer to your MCP client's documentation for configuration file location.
