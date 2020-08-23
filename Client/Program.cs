using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = delegate { return true; },
            };

            using var httpClient = new HttpClient(handler);

            using var response = await httpClient.GetAsync("https://localhost/AspNetCoreBug/WeatherForecast");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.ToString());
            }

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Request was successful, waiting...");

            await Task.Delay(TimeSpan.FromSeconds(60));

            Console.WriteLine("Waiting done, closing");
        }
    }
}
