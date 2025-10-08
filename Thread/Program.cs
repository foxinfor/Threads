using System.Diagnostics;

namespace Threads
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Синхронная");
            double syncTime = SyncProcessData();

            Console.WriteLine("Асинхронная");
            double asyncTime = await ASyncProcessData();

            if (syncTime > asyncTime)
            {
                Console.WriteLine($"Асинхронная обработка быстрее на {syncTime - asyncTime:F2} секунд");
            }
            else if (syncTime < asyncTime)
            {
                Console.WriteLine($"Синхронная обработка быстрее на {asyncTime - syncTime:F2} секунд");
            }
            else
            {
                Console.WriteLine("Оба метода заняли одинаковое время");
            }
        }

        public static string ProcessData(string dataName)
        {
            Console.WriteLine($"Начата обработка {dataName}");


            Stopwatch stopwatch = Stopwatch.StartNew();
            Thread.Sleep(3000);
            stopwatch.Stop();

            return $"Обработка {dataName} завершена за {stopwatch.Elapsed.TotalSeconds} секунд";
        }

        public static double SyncProcessData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            string output1 = ProcessData("Файл 1");
            Console.WriteLine($"{output1}");

            string output2 = ProcessData("Файл 2");
            Console.WriteLine($"{output2}");

            string output3 = ProcessData("Файл 3");
            Console.WriteLine($"{output3}");

            stopwatch.Stop();

            double timeOfWork = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Суммарное время выполнения синхронных методов равно {timeOfWork:F2} секунд");
            return timeOfWork;
        }

        public async static Task<string> ProcessDataAsync(string dataName)
        {
            Console.WriteLine($"Начата обработка {dataName}");

            Stopwatch stopwatch = Stopwatch.StartNew();
            await Task.Delay(3000);
            stopwatch.Stop();

            return $"Обработка {dataName} завершена за {stopwatch.Elapsed.TotalSeconds} секунд";
        }

        public async static Task<double> ASyncProcessData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var task1 = ProcessDataAsync("Файл 1");
            var task2 = ProcessDataAsync("Файл 2");
            var task3 = ProcessDataAsync("Файл 3");

            var results = await Task.WhenAll(task1, task2, task3);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            stopwatch.Stop();

            double workTime = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Суммарное время выполнения асинхронных методов: {workTime:F2} секунд");

            return workTime;
        }
    }
}
