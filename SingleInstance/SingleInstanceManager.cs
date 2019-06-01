using NamedPipeWrapper;
using System;
using System.Threading;

namespace SingleInstance
{
    public class SingleInstanceManager : IDisposable
    {
        private static Mutex mutex;

        private readonly string appKey;

        private NamedPipeServer<object> server;

        public SingleInstanceManager(string appKey, TimeSpan timeout)
        {
            this.appKey = appKey;
            Check(timeout);
        }

        public delegate void NewInstanceHandler();

        public event NewInstanceHandler OnNewInstance;

        public bool FirstInstance { get; private set; }

        private bool Check(TimeSpan timeout)
        {
            mutex = new Mutex(true, appKey);
            FirstInstance = mutex.WaitOne(timeout);
            if (FirstInstance)
            {
                StartServer();
            }
            else
            {
                StartClient();
            }
            return FirstInstance;
        }

        public void Release()
        {
            if (FirstInstance)
            {
                mutex.ReleaseMutex();
            }
            server?.Stop();
        }

        public void Dispose()
        {
            Release();
        }

        private void StartClient()
        {
            var sender = new NamedPipeClient<object>(appKey);
            sender.Start();
            sender.Stop();
        }

        private void StartServer()
        {
            server = new NamedPipeServer<object>(appKey);
            server.ClientConnected += (_) => OnNewInstance?.Invoke();
            server.Start();
        }
    }
}