using IProductN;
using ProductN;

namespace ProductService {

    public class ProductService : IProduct<int>
    {

        public List<Product<int>> products;

        public void Create(Product<int> p)
        {
            products.Add(p);
            throw new NotImplementedException();
        }

        public void Delete<Tkey>(Tkey id)
        {
            throw new NotImplementedException();
        }

        public List<Product<Tkey>> FilterByCriteria<Tkey>()
        {
            throw new NotImplementedException();
        }

        public Product<Tkey> FindById<Tkey>(Tkey id)
        {
            throw new NotImplementedException();
        }

        public List<Product<int>> GetListAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Product<int> p)
        {
            throw new NotImplementedException();
        }
    }

}