public class NullNullable
{
    class ABC {
        
        private int Value {get; set;}

        public void FuncGett() => Console.WriteLine("abccccccc", Value);

    }
    public static void Main(string[] args)
    {
        // null
        int? age;
        age = null;

        var obj = new {name = "ssvvv", age };
        if (obj.age.HasValue) 
            Console.WriteLine($"log: {obj.age}");
        else
            Console.WriteLine("error: field is null ");

        // nullable
        ABC n = null;
        n?.FuncGett();
    }
}