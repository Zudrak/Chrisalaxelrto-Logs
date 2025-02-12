using System.Runtime.CompilerServices;

public class MockJournalService : IJournalService
{
    public async IAsyncEnumerable<string> StreamJournalEntries([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int counter = 1;

        while (!cancellationToken.IsCancellationRequested )
        {
            await Task.Delay(1000); // Simulate delay
            yield return $"Mock entry {counter++}";
        }
    }
}