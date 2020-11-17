using System;
using System.Net.Http;
using CacheCow.Client;
using CacheCow.Client.RedisCacheStore;

namespace CacheMoneyClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            var client = ClientExtensions.CreateClient(new RedisStore("localhost:6379"));
            var baseAddress = new Uri("http://localhost:1337");
            client.BaseAddress = baseAddress;

            while (true)
            {
                Console.WriteLine("If he loves me, why hasnt he called me");
                Console.ReadLine();
                var response = await client.GetAsync("/cache/time");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Headers.CacheControl.ToString());
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                Console.WriteLine("Type 'donezo' to quit");
                if (Console.ReadLine() == "donezo") break;
            }
        }
    }
}
