using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        // ProductImagesController sınıfının kurucu metodudur.
        public ProductImagesController(IProductImageService productImageService)
        {
            // IProductImageService arayüzünü uygulayan bir servisi alır.
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductImageList()
        {
            // Tüm ürün resimlerini getiren metot çağrılır.
            var values = await _productImageService.GetAllProductImageAsync();

            // Eğer ürün resmi listesi boşsa, NotFound döner.
            if (values == null || !values.Any())
            {
                return NotFound("Ürün resmi bulunamadı!");
            }

            // Ürün resmi listesi başarılı bir şekilde alındıysa, Ok döner.
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(string id)
        {
            // Belirli bir ID'ye göre ürün resmini getiren metot çağrılır.
            var values = await _productImageService.GetByIdProductImageAsync(id);

            // Eğer ürün resmi bulunamazsa, NotFound döner.
            if (values == null)
            {
                return NotFound("Ürün resmi bulunamadı!");
            }

            // Ürün resmi bulunduysa, Ok döner.
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            // Yeni ürün resmi oluşturma metodu çağrılır.
            await _productImageService.CreateProductImageAsync(createProductImageDto);

            // Ürün resmi başarıyla eklendiyse, Ok döner.
            return Ok("Ürün resmi başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            // Silinecek ürün resminin var olup olmadığını kontrol et.
            var image = await _productImageService.GetByIdProductImageAsync(id);

            // Eğer ürün resmi bulunamazsa, NotFound döner.
            if (image == null)
                return NotFound("Ürün resmi bulunamadı!");

            // Ürün resmi bulunduysa, silme işlemini gerçekleştir.
            await _productImageService.DeleteProductImageAsync(id);

            // Silme işlemi başarılıysa, Ok döner.
            return Ok("Ürün resmi başarıyla silindi!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            // Güncellenecek ürün resminin var olup olmadığını kontrol et.
            var image = await _productImageService.GetByIdProductImageAsync(updateProductImageDto.ProductImageID);

            // Eğer ürün resmi bulunamazsa, NotFound döner.
            if (image == null)
                return NotFound("Ürün resmi bulunamadı!");

            // Ürün resmi bulunduysa, güncelleme işlemini gerçekleştir.
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);

            // Güncelleme işlemi başarılıysa, Ok döner.
            return Ok("Ürün resmi başarıyla güncellendi!");
        }
    }
}
