using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        // CategoriesController sınıfının kurucu metodudur.
        public CategoriesController(ICategoryService categoryService)
        {
            // ICategoryService arayüzünü uygulayan bir servisi alır.
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            // Tüm kategorileri getiren metot çağrılır.
            var values = await _categoryService.GetAllCategoryAsync();

            // Eğer kategori listesi boşsa, NotFound döner.
            if (values == null || !values.Any())
            {
                return NotFound("Kategori bulunamadı!");
            }

            // Kategori listesi başarılı bir şekilde alındıysa, Ok döner.
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            // Belirli bir ID'ye göre kategoriyi getiren metot çağrılır.
            var values = await _categoryService.GetByIdCategoryAsync(id);

            // Eğer kategori bulunamazsa, NotFound döner.
            if (values == null)
            {
                return NotFound("Kategori bulunamadı!");
            }

            // Kategori bulunduysa, Ok döner.
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            // Yeni kategori oluşturma metodu çağrılır.
            await _categoryService.CreateCategoryAsync(createCategoryDto);

            // Kategori başarıyla eklendiyse, Ok döner.
            return Ok("Kategori başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            // Silinecek kategorinin var olup olmadığını kontrol et.
            var category = await _categoryService.GetByIdCategoryAsync(id);

            // Eğer kategori bulunamazsa, NotFound döner.
            if (category == null)
                return NotFound("Kategori bulunamadı!");

            // Kategori bulunduysa, silme işlemini gerçekleştir.
            await _categoryService.DeleteCategoryAsync(id);

            // Silme işlemi başarılıysa, Ok döner.
            return Ok("Kategori başarıyla silindi!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            // Güncellenecek kategorinin var olup olmadığını kontrol et.
            var category = await _categoryService.GetByIdCategoryAsync(updateCategoryDto.CategoryID);

            // Eğer kategori bulunamazsa, NotFound döner.
            if (category == null)
                return NotFound("Kategori bulunamadı!");

            // Kategori bulunduysa, güncelleme işlemini gerçekleştir.
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);

            // Güncelleme işlemi başarılıysa, Ok döner.
            return Ok("Kategori başarıyla güncellendi!");
        }
    }
}
