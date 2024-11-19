namespace ProductN
{
    public class Product<TkeyId> {
        private TkeyId Id { get; set; }
        private string Name { get; set; }
        
        private string Price { get; set; }
        
        public Product(TkeyId id, string name, string price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }
}