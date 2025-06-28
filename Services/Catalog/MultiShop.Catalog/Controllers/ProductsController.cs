using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // ProductsController sınıfının kurucu metodudur.
        public ProductsController(IProductService productService)
        {
            // IProductService arayüzünü uygulayan bir servisi alır.
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            // Tüm ürünleri getiren metot çağrılır.
            var values = await _productService.GetAllProductAsync();

            // Eğer ürün listesi boşsa, NotFound döner.
            if (values == null || !values.Any())
            {
                return NotFound("Ürün bulunamadı!");
            }

            // Ürün listesi başarılı bir şekilde alındıysa, Ok döner.
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            // Belirli bir ID'ye göre ürünü getiren metot çağrılır.
            var values = await _productService.GetByIdProductAsync(id);

            // Eğer ürün bulunamazsa, NotFound döner.
            if (values == null)
            {
                return NotFound("Ürün bulunamadı!");
            }

            // Ürün bulunduysa, Ok döner.
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            // Yeni ürün oluşturma metodu çağrılır.
            await _productService.CreateProductAsync(createProductDto);

            // Ürün başarıyla eklendiyse, Ok döner.
            return Ok("Ürün başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            // Silinecek ürünün var olup olmadığını kontrol et.
            var product = await _productService.GetByIdProductAsync(id);

            // Eğer ürün bulunamazsa, NotFound döner.
            if (product == null)
                return NotFound("Ürün bulunamadı!");

            // Ürün bulunduysa, silme işlemini gerçekleştir.
            await _productService.DeleteProductAsync(id);

            // Silme işlemi başarılıysa, Ok döner.
            return Ok("Ürün başarıyla silindi!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            // Güncellenecek ürünün var olup olmadığını kontrol et.
            var product = await _productService.GetByIdProductAsync(updateProductDto.ProductID);

            // Eğer ürün bulunamazsa, NotFound döner.
            if (product == null)
                return NotFound("Ürün bulunamadı!");

            // Ürün bulunduysa, güncelleme işlemini gerçekleştir.
            await _productService.UpdateProductAsync(updateProductDto);

            // Güncelleme işlemi başarılıysa, Ok döner.
            return Ok("Ürün başarıyla güncellendi!");
        }
    }
}
