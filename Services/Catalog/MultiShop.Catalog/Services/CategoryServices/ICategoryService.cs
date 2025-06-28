using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {

        // Tüm kategorileri getiren metot
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();

        // Kategori oluşturma, güncelleme, silme ve ID ile getirme işlemleri için metotlar
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(string id);
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
    }
}
