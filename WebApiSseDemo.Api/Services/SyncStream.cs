using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using WebApiSseDemo.Api.Models;

namespace WebApiSseDemo.Api.Services;

public static class SyncStream
{
	public static async IAsyncEnumerable<SseItem<SyncProgress>> StreamProgressAsync(
		[EnumeratorCancellation] CancellationToken ct)
	{
		const int total = 10;

		for (int i = 1; i <= total; i++)
		{
			await Task.Delay(500, ct);

			yield return new SseItem<SyncProgress>(
				new SyncProgress(i, total, $"Synced plant #{i}"),
				eventType: "progress")
			{
				EventId = i.ToString()
			};
		}

		yield return new SseItem<SyncProgress>(new SyncProgress(total, total, "All done"), eventType: "complete")
		{
			EventId = (total + 1).ToString()
		};
	}
}
