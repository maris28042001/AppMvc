using AppMvc.Models;

namespace AppMvc.Services{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[]{
                new ProductModel() {Id = 1, Name = "Iphone X", Price = 110},
                new ProductModel() {Id = 2, Name = "Samsung ", Price = 10},
                new ProductModel() {Id = 3, Name = "Sony", Price = 220},
            });
        }
    }
}
