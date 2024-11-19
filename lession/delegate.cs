public class Delegate
{
    public static int CalculateSum(int[] nums) => nums.Sum();
    public static void LogSum(int sum) => Console.WriteLine($"sum: {sum}");
    public static void Main(string[] args)
    {
        // Func delegate : type function có kiểu trả về
        Func<int[], int> calculateSum = CalculateSum; // delegate int KIEU(int[] nums)
        // Action Func : type function KO có kiểu trả về (void)
        Action<int> logShowSum = LogSum; // delegate void KIEU(int sum)
        logShowSum(calculateSum([3,5,9]));
    }
}