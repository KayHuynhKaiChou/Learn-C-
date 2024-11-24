using BrandN;
using ProductN;

public class Linq
{
    public static void Main(string[] args)
    {
        var brands = new List<Brand<int>>() 
        {
            new(1, "Công ty AAA"),
            new(2, "Công ty BBB"),
            new(3, "Công ty CCC"),
            new(4, "Công ty DDD")
        };

        var products = new List<Product<int>>() 
        {
            new (1, "Bàn trà",    400, ["Gray", "Green"],          2),
            new (2, "Tranh treo", 400, ["Yellow", "Green"],        1),
            new (3, "Đèn trùm",   500, ["White"],                  3),
            new (4, "Bàn học",    200, ["White", "Green"],         1),
            new (5, "Túi da",     300, ["Red", "Black", "Yellow"], 2),
            new (6, "Giường ngủ", 500, ["White"],                  2),
            new (7, "Tủ áo",      600, ["White"],                  null),
        };

        // query sql by linq
        var resultQuery1 = from p in products
                    where p.Price >= 400
                    select new {
                        p.Name, // Name = p.Name nhưng viết gọn
                        p.Price
                    };
        // Select
        var resultQuery2 = products.Select(p => p.Name);

        // Where
        var resultQuery3 = products.Where(p => p.Colors.Contains("White"));

        // Join
        var resultQuery4 = products.Join(
            brands,
            p => p.Brand,
            b => b.Id,
            (p, b) => new {
                NameProduct = p.Name,
                NameBrand = b.Name
            }
        );

        // GroupJoin
        var resultQuery5 = brands.GroupJoin(
            products,
            b => b.Id,
            p => p.Brand,
            (brand, productsByBrand) => new {
                NameBrand = brand.Name,
                NameProducts = productsByBrand.Any() 
                                    ? $"[ {string.Join(", ", productsByBrand.Select(p => p.Name))}]" 
                                    : "Not product"
            }
        );

        // Take
        var resultQuery6 = products.Take(2);

        // Skip
        var resultQuery7 = products.Skip(1);

        // OrderBy (ascending) <-> OrderByDescending
        var resultQuery8 = products.OrderBy(p => p.Price);

        // GroupBy
        var resultQuery9 = products.GroupBy(p => p.Brand);
        foreach (var group in resultQuery9) {
            Console.WriteLine($"Brand Id {group.Key}: ");
            foreach (var p in group) {
                Console.WriteLine(p);
            }
        }

        // SelectMany ( thấy chỉ apply cho field là 1 array ) + District
        var resultQuery10 = products.SelectMany(p => p.Colors).Distinct();

        // query product mau xanh co price < 500
        var resultQuery11 = from p in products
                            where p.Price < 500 && p.Colors.Contains("Green")
                            orderby p.Price descending
                            select p;
        // from p in products
        // from color in p.Colors
        // where p.Price < 500 && color == "Green"

        // group các products by price
        var resultQuery12 = from p in products
                            group p by p.Price;

        // group các products by price va sort by descending
        var resultQuery13 = from p in products
                            group p by p.Price into gr
                            orderby gr.Key descending
                            select gr;
        Console.WriteLine("------------------------------------");
        foreach (var group in resultQuery12)
        {
            Console.WriteLine($"Price {group.Key}");
            group.ToList().ForEach(Console.WriteLine);
        }

        // let variable in linq 
        var resultQuery14 = from p in products
                            group p by p.Price into gr
                            let sl = gr.Count() // sl is let variable
                            orderby gr.Key
                            select new {
                                Price = gr.Key,
                                Length = sl,
                                ProductsByPrice = gr 
                            };
        Console.WriteLine("------------------------------------");
        resultQuery14.ToList().ForEach(res => {
            Console.WriteLine($"Price {res.Price}");
            Console.WriteLine($"Total of products by price: {res.Length}");
            res.ProductsByPrice.ToList().ForEach(Console.WriteLine);
        });

        // join in linq
        var resultQuery15 = from p in products
                            join b in brands on p.Brand equals b.Id
                            select new {
                                p.Name,
                                Brand = b.Name,
                                p.Price
                            };

        // left join in linq
        var resultQuery16 = from p in products
                            join b in brands on p.Brand equals b.Id into pb
                            from brand in pb.DefaultIfEmpty()
                            select new {
                                p.Name,
                                Brand = (brand != null) ? brand.Name : "Not brand",
                                p.Price
                            };

        var resultQuery17 = from brand in brands
                     join product in products
                     on brand.Id equals product.Brand into productGroup
                     from product in productGroup.DefaultIfEmpty()
                     select new
                     {
                         ProductName = product?.Name ?? "No Product",
                         BrandName = brand.Name
                     };
        Console.WriteLine("------------------------------------");
        foreach (var item in resultQuery17)
        {
            Console.WriteLine($"BrandName: {item.BrandName}, ProductName: {item.ProductName}");
        }

        var resultQuery18 = brands
            .GroupJoin(
                products,
                brand => brand.Id,
                product => product.Brand,
                (brand, productGroup) => new { Brand = brand, Products = productGroup.DefaultIfEmpty() }
            )
            .SelectMany(
                bp => bp.Products,
                (bp, product) => new
                {
                    BrandName = bp.Brand.Name,
                    ProductName = product?.Name ?? "No Product"
                }
            )
            .ToList();

        //foreach
        Console.WriteLine("------------------------------------");
        foreach (var q in resultQuery18)
        {
            Console.WriteLine(q);
        };

        // foreach (Product<int> p in products)
        // {
        //     Console.WriteLine(p);
        // };
    }
}