using System.ComponentModel.DataAnnotations;
using ObjCommonN;

namespace ProductN
{
    public class Product<TkeyId> : ObjCommon<TkeyId>
    {
        [Required(ErrorMessage = "Name is required !")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "lenth of name is at least 3 characters and max is 50 characters !")]
        public string Name {set; get;}         // tên

        [Required(ErrorMessage = "Price is required !")]
        [Range(1000, 1000000, ErrorMessage = "Price must be in range from 1K to 1M !")]
        public double Price {set; get;}        // giá
        public string[] Colors {set; get;}     // các màu sắc
        public int? Brand {set; get;}           // ID Nhãn hiệu, hãng
        public Product(TkeyId id, string name, double price, string[] colors, int? brand) : base(id)
        {
            Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
        {
            return $"{Id,3} {Name,12} {Price,5} {Brand,2} [{string.Join(",", Colors)}]";
        }
    }
}