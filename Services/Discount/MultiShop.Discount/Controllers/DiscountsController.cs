using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;
using System.Runtime.InteropServices;

namespace MultiShop.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        // DiscountsController sınıfının kurucu metodudur.
        public DiscountsController(IDiscountService discountService)
        {
            // IDiscountService arayüzünü uygulayan bir servisi alır.
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            // Tüm kuponları getiren metot çağrılır.
            var values = await _discountService.GetAllDiscountCouponAsync();

            // Eğer kupon listesi boşsa, NotFound döner.
            if (values == null || !values.Any())
            {
                return NotFound("İndirim kuponu bulunamadı!");
            }

            // Kupon listesi başarılı bir şekilde alındıysa, Ok döner.
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
            // Belirli bir ID'ye göre kuponu getiren metot çağrılır.
            var values = await _discountService.GetByIdDiscountCouponAsync(id);

            // Eğer kupon bulunamazsa, NotFound döner.
            if (values == null)
            {
                return NotFound("İndirim kuponu bulunamadı!");
            }

            // Kupon bulunduysa, Ok döner.
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
        {
            // Yeni kupon oluşturma metodu çağrılır.
            await _discountService.CreateDiscountCouponAsync(createCouponDto);

            // Kupon başarıyla eklendiyse, Ok döner.
            return Ok("İndirim kuponu başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            // Belirli bir ID'ye sahip kuponu silen metot çağrılır.
            var coupon = await _discountService.GetByIdDiscountCouponAsync(id);

            // Eğer kupon bulunamazsa, NotFound döner.
            if (coupon == null)
                return NotFound("İndirim kuponu bulunamadı!");

            // Kupon başarıyla bulunduysa, silme işlemi gerçekleştirilir.
            await _discountService.DeleteDiscountCouponAsync(id);

            // Silme işlemi başarılıysa, Ok döner.
            return Ok("İndirim kuponu başarıyla silindi!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
        {
            // Güncellenecek kuponun var olup olmadığını kontrol et
            var coupon = await _discountService.GetByIdDiscountCouponAsync(updateCouponDto.CouponID);

            // Eğer kupon bulunamazsa, NotFound döner.
            if (coupon == null)
                return NotFound("İndirim kuponu bulunamadı!");

            // Kupon güncelleme işlemi gerçekleştirilir.
            await _discountService.UpdateDiscountCouponAsync(updateCouponDto);

            // Güncelleme işlemi başarılıysa, Ok döner.
            return Ok("İndirim kuponu başarıyla güncellendi!");
        }
    }
}
