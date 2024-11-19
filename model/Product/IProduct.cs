using ProductN;

namespace IProductN {
    public interface IProduct<TkeyId>
    {
        public void Create(Product<TkeyId> p);
        public Product<Tkey> FindById<Tkey>(Tkey id);
        public List<Product<Tkey>> FilterByCriteria<Tkey>();
        public List<Product<TkeyId>> GetListAll();
        public void Update(Product<TkeyId> p);
        public void Delete<Tkey>(Tkey id);

    }
}