using System.Net.ServerSentEvents;
using WebApiSseDemo.Api.Models;

namespace WebApiSseDemo.Api.Services;

public delegate IAsyncEnumerable<SseItem<SyncProgress>> StreamSyncProgress(CancellationToken ct);
