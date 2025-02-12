using System.Diagnostics;
using System.Runtime.CompilerServices;

public class JournalService : IJournalService
{
    public async IAsyncEnumerable<string> StreamJournalEntries([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "journalctl",
            Arguments = "-u ukulele -f",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };
        process.Start();

        using var reader = process.StandardOutput;
        while (!process.HasExited || !reader.EndOfStream || !cancellationToken.IsCancellationRequested )
        {
            var line = await reader.ReadLineAsync();
            if (line != null)
            {
                yield return line;
            }
        }
    }

    public async Task<string> GetAllJournalEntries(CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(Path.GetTempPath(), "ukulele.logs");

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "journalctl",
            Arguments = $"-u ukulele > {filePath}",
            RedirectStandardOutput = false,
            UseShellExecute = true,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };
        process.Start();
        await process.WaitForExitAsync(cancellationToken);

        return filePath;
    }
}