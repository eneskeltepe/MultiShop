using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        // Tüm ürün resimlerini getiren metot.
        Task<List<ResultProductImageDto>> GetAllProductImageAsync();

        // Yeni bir ürün resmi oluşturan metot.
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);

        // Ürün resmini güncelleyen metot.
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);

        // Ürün resmini silen metot.
        Task DeleteProductImageAsync(string id);

        // Belirli bir ID'ye göre ürün resmi getiren metot.
        Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id);
    }
}
