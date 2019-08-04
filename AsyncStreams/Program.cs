using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncStreams
{
    class Program
    {
        private static CancellationTokenSource _cancellationtokenSource;
        
        static async Task Main(string[] args)
        {
            _cancellationtokenSource = new CancellationTokenSource();
            
            await foreach (var number in GenerateSequence(_cancellationtokenSource.Token))
            {
                Console.WriteLine(number);
            }
        }

        private static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence(CancellationToken token)
        {
            var bufferBlock = new BufferBlock<int>();
            var task = Task.Run(() => GenerateSequenceToBuffer(bufferBlock));

            Console.WriteLine("Waiting...");

            while (!token.IsCancellationRequested)
            {
                var data = 0;
                
                try
                {
                    data = await bufferBlock.ReceiveAsync(token);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Task Cancelled...");
                }

                Console.WriteLine($"{data} Received...");
                yield return data;
            }


            Console.WriteLine("Complete...");

            task.Wait();
        }

        private static void GenerateSequenceToBuffer(BufferBlock<int> bufferBlock)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"{DateTime.Now:fff}: Delaying 100ms");
                Thread.Sleep(100);
                Console.WriteLine($"{DateTime.Now:fff}: Returning {i}");
                bufferBlock.Post(i);
                Console.WriteLine($"{DateTime.Now:fff}: Returned {i}");
            }
            
            Console.WriteLine("All item sent to buffer...");
            Thread.Sleep(5000);
            _cancellationtokenSource.Cancel();
        }
    }
}
