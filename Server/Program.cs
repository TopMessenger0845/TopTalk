using System.Net;
using TopNetwork.RequestResponse;
using TopTalkLogic.Core.Models;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), 5335);

            TopTalkServer server = new TopTalkServer() { logger = msg => _ = ConsoleLogger.LogLine(msg) };
            server.SetEndPoint(endPoint);

            _ = server.StartServer();
            await ConsoleLogger.LogLine("Чтоб остановить сервер, нажмите любую кнопку...", ConsoleColor.Yellow);
            Console.ReadKey(true);

            await server.StopServer();
        }

        public static class ConsoleLogger
        {
            private static SemaphoreSlim _consoleSemaphore = new(1, 1);
            public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

            public static async Task LogLine(string msg, ConsoleColor color = ConsoleColor.White)
                => await ConsoleLog(msg, Console.WriteLine, color);

            public static async Task Log(string msg, ConsoleColor color = ConsoleColor.White)
                => await ConsoleLog(msg, Console.Write, color);

            public static async Task ConsoleLog(string msg, LogString consoleLogger, ConsoleColor color = ConsoleColor.White)
            {
                await _consoleSemaphore.WaitAsync();
                try
                {
                    Console.ForegroundColor = color;
                    consoleLogger(msg);
                }
                finally { Console.ForegroundColor = DefaultColor; _consoleSemaphore.Release(); }
            }
        }
    }
}
