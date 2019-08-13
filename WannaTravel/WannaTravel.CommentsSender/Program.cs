using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WannaTravel.CommentsSender
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    System.Console.WriteLine("Application start....");
                    Thread.Sleep(8000);
                    System.Console.WriteLine("Creating tasks....");

                    var test = Enumerable.Range(1, 35).Select(x =>
                    Task.Run(async () =>
                   {
                        using (var client = new HttpClient(handler))
                        {
                           
                            var content = new StringContent(CommentGenerator.GenerateRandomComment(), Encoding.UTF8, "application/json");
                            await client.PostAsync(@"http://localhost:53324/api/Comment/", content);
                        }
                    }))
                    .ToArray();

                    System.Console.WriteLine("Tasks created. Processing.......");

                    Task.WaitAll(test);
                    System.Console.WriteLine("First pack of comments were sent.");

                }

                using (var handler = new HttpClientHandler())
                {
                    System.Console.WriteLine("Preparing for second bundle...");
                    Thread.Sleep(8000);
                    System.Console.WriteLine("Creating tasks....");

                    var test = Enumerable.Range(1, 35).Select(x =>
                    Task.Run(async () =>
                    {
                        using (var client = new HttpClient(handler))
                        {
                            var content = new StringContent(CommentGenerator.GenerateRandomComment(), Encoding.UTF8, "application/json");
                            await client.PostAsync(@"http://localhost:53324/api/Comment/", content);
                        }
                    }))
                    .ToArray();
                    System.Console.WriteLine("Tasks created. Processing.......");

                    Task.WaitAll(test);
                    System.Console.WriteLine("Second pack of comments were sent.");
                    System.Console.WriteLine("Done");
                    System.Console.ReadKey();

                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}
