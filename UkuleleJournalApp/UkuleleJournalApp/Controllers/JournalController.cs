using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/journal")]
public class JournalController : ControllerBase
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpGet("stream")]
    public async Task StreamJournal(CancellationToken cancellationToken)
    {
        Response.Headers.Append("Content-Type", "text/event-stream");

        await foreach (var entry in _journalService.StreamJournalEntries().WithCancellation(cancellationToken))
        {
            await Response.WriteAsync($"data: {entry}\n\n");
            await Response.Body.FlushAsync();
        }
    }
}