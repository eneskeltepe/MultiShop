using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        // Tüm ürünleri getiren metot
        Task<List<ResultProductDto>> GetAllProductAsync();

        // Ürün oluşturma, güncelleme, silme ve ID ile getirme işlemleri için metotlar
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
    }
}
