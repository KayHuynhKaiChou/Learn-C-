using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class DependencyInjection {
    interface IClassB {
        public void ActionB();
    }
    interface IClassC {
        public void ActionC();
    }

    class ClassC : IClassC {
        public ClassC() => Console.WriteLine ("ClassC is created");
        public void ActionC() => Console.WriteLine("Action in ClassC");
    }

    class ClassB : IClassB {
        private readonly IClassC c_dependency;
        public ClassB(IClassC classc, string s)
        {
            c_dependency = classc;
            Console.WriteLine($"ClassB is created and string: {s}");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB");
            c_dependency.ActionC();
        }
    }


    class ClassA {
        private readonly IClassB b_dependency;
        // inject IClassB vào ClassA thông qua constructor hay ClassA là 1 dependency của ClassB
        public ClassA(IClassB classb)
        {
            b_dependency = classb;
            Console.WriteLine("ClassA is created");
        }
        public void ActionA()
        {
            Console.WriteLine("Action in ClassA");
            b_dependency.ActionB();
        }
    }

    // Factory nhận tham số là IServiceProvider và trả về đối tượng địch vụ cần tạo
    static ClassB CreateBFactory(IServiceProvider serviceprovider)
    {
        return new ClassB(
            serviceprovider.GetService<IClassC>(), 
            "string class B"
        );
    }

    public class MyServiceOptions
    {
        public string data1 { get; set; }
        public int data2 { get; set; }
    }

    public class MyService {
        public string data1 {get; set;}
        public int data2 {get; set;}

        // Tham số khởi tạo là IOptions, các tham số khởi tạo khác nếu có khai báo như bình thường
        public MyService(IOptions<MyServiceOptions> options)
        {
            // Đọc được MyServiceOptions từ IOptions
            MyServiceOptions opts = options.Value;
            data1 = opts.data1;
            data2 = opts.data2;
        }
        public void PrintData() => Console.WriteLine($"{data1} / {data2}");
    }

    public static void Main(string[] args)
    {
        // DI dùng từ khóa new 
        IClassC objC = new ClassC();
        IClassB objB = new ClassB(objC, "string class B");
        ClassA objA = new ClassA(objB);
        objA.ActionA();
        Console.WriteLine("------------------------");

        // DI dùng lib của asp.net
        var services = new ServiceCollection();

        services.AddSingleton<ClassA, ClassA>();
        services.AddSingleton<IClassB, ClassB>(
            // delegate : 1 là viết thẳng callback trong này hoặc tách ra 1 factory để reuse
            // Sử dụng Factory đăng ký
            // Delegate dưới bạn có thể khai báo thành một phương thức, 
            // một phương thức cung cấp cơ chế để tạo ra đối tượng mong muốn gọi là Factory.
            CreateBFactory
        );
        services.AddSingleton<IClassC, ClassC>();

        var provider = services.BuildServiceProvider();

        var classA = provider.GetService<ClassA>();
        classA.ActionA();

        // inject data vào service by use IOptions
        var services1 = new ServiceCollection();
        services1.Configure<MyServiceOptions>(
            options => {
                options.data1 = "Xin chao cac ban";
                options.data2  = 2024;
            }
        );

        services1.AddSingleton<MyService>();
        var provider1 = services1.BuildServiceProvider();

        var myservice = provider1.GetService<MyService>();
        Console.WriteLine("------------------------");
        myservice.PrintData();
    }
}