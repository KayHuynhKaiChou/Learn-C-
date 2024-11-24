using System.ComponentModel.DataAnnotations;
using ProductN;

public class AttributeC
{
    public static void Main(string[] args)
    {
        Product<int> p = new (12, null, 500, [], 2);

        ValidationContext context = new ValidationContext(p);

        var result = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(p, context, result, true);

        if (isValid == false)
        {
            result.ToList().ForEach(err => {
                Console.WriteLine($"{err.MemberNames.First()}: {err.ErrorMessage}");
            });
        }
    }
}