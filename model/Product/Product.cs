using ObjCommonN;

namespace ProductN
{
    public class Product<TkeyId> : ObjCommon<TkeyId>
    {
        public string Name {set; get;}         // tên
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