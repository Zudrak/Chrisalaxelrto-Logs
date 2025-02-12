using System.Text;
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


    [HttpGet("download")]
    public async Task<IActionResult> DownloadJournal(CancellationToken cancellationToken)
    {
        var entries = await _journalService.GetAllJournalEntries(cancellationToken);
        var stringBuilder = new StringBuilder();

        foreach (var entry in entries)
        {
            stringBuilder.AppendLine(entry);
        }

        var byteArray = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        var stream = new MemoryStream(byteArray);

        return File(stream, "application/octet-stream", "journal.log");
    }
}