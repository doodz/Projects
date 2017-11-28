using System;

namespace ConsolAppYgg
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new YggClientCore.YggClient();
            client.Index().GetAwaiter();
            client.LoginPage().GetAwaiter();
            client.Seach().GetAwaiter();
            Console.ReadKey();
        }
    }
}