using System;
using System.Threading;

namespace AdventureS25
{
    // Listens for Enter key in background, sets SkipToken
    public class SkipListener : IDisposable
    {
        private Thread _thread;
        private bool _running;

        public void Start()
        {
            _running = true;
            _thread = new Thread(ListenLoop) { IsBackground = true };
            _thread.Start();
        }

        private void ListenLoop()
        {
            while (_running)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        SkipToken.RequestSkip();
                        break;
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void Dispose()
        {
            _running = false;
            if (_thread != null && _thread.IsAlive)
                _thread.Join();
        }
    }
}
