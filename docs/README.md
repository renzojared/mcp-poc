# Getting Started

General workflow for MCP development.

## Prerequisites

- Docker with Docker Compose
- Node.js ([check version](https://github.com/modelcontextprotocol/inspector))
- API credentials (see specific MCP docs)

## Workflow

```bash
# 1. Build
docker compose build --no-cache

# 2. Verify (optional quick check)
# Server will start and exit immediately - this is normal
docker run --rm -e ENV_VAR="value" <image-name>
# Look for "Application started" without errors

# 3. Test with Inspector
npx @modelcontextprotocol/inspector \
  -e ENV_VAR="value" \
  -- docker run -i --rm -e ENV_VAR <image-name>

# 4. Configure your MCP client
# See your client's MCP configuration documentation

# 5. Use with your client
```

## Debugging

MCP servers use stdio, making traditional debugging difficult:

- Add logging
- Use Inspector for testing
- Write unit tests

## Available MCPs

- **[Invoice Sync MCP](./mcp-invoice-sync.md)** - Invoice Sync integration

## Resources

- [MCP Docs](https://modelcontextprotocol.io/)
- [Inspector](https://github.com/modelcontextprotocol/inspector)
