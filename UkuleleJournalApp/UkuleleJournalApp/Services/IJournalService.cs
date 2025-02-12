using System.Runtime.CompilerServices;

public interface IJournalService
{
    IAsyncEnumerable<string> StreamJournalEntries([EnumeratorCancellation] CancellationToken cancellationToken = default);
}