using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    // İndirim (kupon) işlemlerini yöneten servis sınıfı
    public class DiscountService : IDiscountService
    {
        // Veritabanı bağlantısı için kullanılan context
        private readonly DapperContext _context;

        // DiscountService sınıfının kurucu metodu
        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        // Yeni bir kupon oluşturur ve veritabanına ekler
        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
        {
            string query = "insert into Coupons (Code, Rate, IsActive, ValidDate) values (@Code, @Rate, @IsActive, @ValidDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Code", createCouponDto.Code);
            parameters.Add("Rate", createCouponDto.Rate);
            parameters.Add("IsActive", createCouponDto.IsActive);
            parameters.Add("ValidDate", createCouponDto.ValidDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // Belirtilen ID'ye sahip kuponu veritabanından siler
        public async Task DeleteDiscountCouponAsync(int id)
        {
            string query = "delete from Coupons where CouponID = @CouponID";
            var parameters = new DynamicParameters();
            parameters.Add("CouponID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // Tüm kuponları veritabanından çeker ve liste olarak döner
        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
        {
            string query = "Select * from Coupons";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        // Belirli bir ID'ye sahip kuponun detaylarını getirir
        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
        {
            string query = "Select * from Coupons where CouponID = @CouponID";
            var parameters = new DynamicParameters();
            parameters.Add("CouponID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);
                return values;
            }
        }

        // Var olan bir kuponun bilgilerini günceller
        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto)
        {
            string query = "Update Coupons Set Code = @Code, Rate = @Rate, IsActive = @IsActive, ValidDate = @ValidDate where CouponID = @CouponID";
            var parameters = new DynamicParameters();
            parameters.Add("CouponID", updateCouponDto.CouponID);
            parameters.Add("Code", updateCouponDto.Code);
            parameters.Add("Rate", updateCouponDto.Rate);
            parameters.Add("IsActive", updateCouponDto.IsActive);
            parameters.Add("ValidDate", updateCouponDto.ValidDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}