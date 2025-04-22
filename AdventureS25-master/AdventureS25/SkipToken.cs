using System.Threading;

namespace AdventureS25
{
    // Helper for skip logic in TextPrinter
    public static class SkipToken
    {
        private static int _skipRequested = 0;

        public static void Reset()
        {
            Interlocked.Exchange(ref _skipRequested, 0);
        }

        public static void RequestSkip()
        {
            Interlocked.Exchange(ref _skipRequested, 1);
        }

        public static bool SkipRequested => Interlocked.CompareExchange(ref _skipRequested, 0, 0) == 1;
    }
}
