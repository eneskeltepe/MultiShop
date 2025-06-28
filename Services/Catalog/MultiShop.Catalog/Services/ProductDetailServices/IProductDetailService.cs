using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public interface IProductDetailService
    {
        // Tüm ürün detaylarını getiren metot
        Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();

        // Ürün detaylarını oluşturma, güncelleme, silme ve ID ile getirme işlemleri için metotlar
        Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        Task DeleteProductDetailAsync(string id);
        Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id);
    }
}
