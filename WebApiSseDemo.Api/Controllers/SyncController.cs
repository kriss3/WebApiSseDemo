using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiSseDemo.Api.Models;
using WebApiSseDemo.Api.Services;

namespace WebApiSseDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncController(StreamSyncProgress stream) : ControllerBase
{
	[HttpGet("stream")]
	public ServerSentEventsResult<SyncProgress> Stream(CancellationToken ct)
		=> TypedResults.ServerSentEvents(stream(ct));
}
