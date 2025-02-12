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

    public async Task<IEnumerable<string>> GetAllJournalEntries(CancellationToken cancellationToken = default)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "journalctl",
            Arguments = "-u ukulele",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };
        process.Start();

        var entries = new List<string>();
        using var reader = process.StandardOutput;
        while (!process.HasExited || !reader.EndOfStream || !cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync();
            if (line != null)
            {
                entries.Add(line);
            }
        }

        return entries;
    }
}