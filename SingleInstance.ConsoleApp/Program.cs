using System;

namespace SingleInstance.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string appKey = "a78dd11e-aec3-4c68-aa5a-4410c51d45fd";
            using (var singleInstance = new SingleInstanceManager(appKey, TimeSpan.Zero))
            {
                singleInstance.OnNewInstance += SingleInstance_OnNewInstance;
                if (singleInstance.FirstInstance)
                {
                    StartApp();
                }
                else
                {
                    singleInstance.SendNofitication();
                    AppStopped();
                }
            }
        }

        private static void SingleInstance_OnNewInstance()
        {
            Console.WriteLine("New app instance detected!");
        }

        private static void StartApp()
        {
            Console.WriteLine("App started...");
            Console.ReadKey();
        }

        private static void AppStopped()
        {
            Console.WriteLine("App stopped!");
            Console.ReadKey();
        }
    }
}