using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;

        // ProductDetailsController sınıfının kurucu metodudur.
        public ProductDetailsController(IProductDetailService productDetailService)
        {
            // IProductDetailService arayüzünü uygulayan bir servisi alır.
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            // Tüm ürün detaylarını getiren metot çağrılır.
            var values = await _productDetailService.GetAllProductDetailAsync();

            // Eğer ürün detayları listesi boşsa, NotFound döner.
            if (values == null || !values.Any())
            {
                return NotFound("Ürün detayı bulunamadı!");
            }

            // Ürün detayları listesi başarılı bir şekilde alındıysa, Ok döner.
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            // Belirli bir ID'ye göre ürün detayını getiren metot çağrılır.
            var values = await _productDetailService.GetByIdProductDetailAsync(id);

            // Eğer ürün detayı bulunamazsa, NotFound döner.
            if (values == null)
            {
                return NotFound("Ürün detayı bulunamadı!");
            }

            // Ürün detayı bulunduysa, Ok döner.
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            // Yeni ürün detayı oluşturma metodu çağrılır.
            await _productDetailService.CreateProductDetailAsync(createProductDetailDto);

            // Ürün detayı başarıyla eklendiyse, Ok döner.
            return Ok("Ürün detayı başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            // Silinecek ürün detayının var olup olmadığını kontrol et.
            var detail = await _productDetailService.GetByIdProductDetailAsync(id);

            // Eğer ürün detayı bulunamazsa, NotFound döner.
            if (detail == null)
                return NotFound("Ürün detayı bulunamadı!");

            // Ürün detayı bulunduysa, silme işlemini gerçekleştir.
            await _productDetailService.DeleteProductDetailAsync(id);

            // Silme işlemi başarılıysa, Ok döner.
            return Ok("Ürün detayı başarıyla silindi!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            // Güncellenecek ürün detayının var olup olmadığını kontrol et.
            var detail = await _productDetailService.GetByIdProductDetailAsync(updateProductDetailDto.ProductDetailID);

            // Eğer ürün detayı bulunamazsa, NotFound döner.
            if (detail == null)
                return NotFound("Ürün detayı bulunamadı!");

            // Ürün detayı bulunduysa, güncelleme işlemini gerçekleştir.
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);

            // Güncelleme işlemi başarılıysa, Ok döner.
            return Ok("Ürün detayı başarıyla güncellendi!");
        }
    }
}
