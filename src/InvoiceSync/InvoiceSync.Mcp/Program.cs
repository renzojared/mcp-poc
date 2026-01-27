using InvoiceSync.Contracts;
using InvoiceSync.Mcp.Features.GetEventDeliveryDetails;
using InvoiceSync.Mcp.Features.GetEventStatus;
using InvoiceSync.Mcp.Features.ListSubscriptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddInvoiceSyncApiClient()
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<GetEventStatusTool>()
    .WithTools<GetEventDeliveryDetailsTool>()
    .WithTools<ListSubscriptionsTool>();

var app = builder.Build();

await app.RunAsync();