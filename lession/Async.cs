public class Async
{
    public static async Task SetTimeout(int timeWait, Action callback) {
        for (int i = 0; i <= timeWait / 1000; i++) {
            await Task.Delay(1000);
            Console.WriteLine($"Task {timeWait} : {i * 1000}");
        }
        callback?.Invoke();
    }
    public static async Task Main(string[] args)
    {
        Task task1 = SetTimeout(
            2000,
            () => Console.WriteLine("task 1 Done!")
        );
        await task1;
        
        Task task2 = SetTimeout(
            5000,
            () => Console.WriteLine("task 2 Done!")
        );
        await task2;
        //await Task.WhenAll(task1, task2);
        Console.WriteLine("Done All !");
    }
}