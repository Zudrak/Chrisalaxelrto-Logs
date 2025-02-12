using System.Runtime.CompilerServices;

public class MockJournalService : IJournalService
{
    public Task<IEnumerable<string>> GetAllJournalEntries(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<string>>(new List<string> { "Mock entry 1", "Mock entry 2" });
    }

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