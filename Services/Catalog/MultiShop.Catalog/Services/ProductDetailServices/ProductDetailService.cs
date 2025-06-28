using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;
        private readonly IMapper _mapper;

        // ProductDetailService sınıfının kurucu metodudur.
        public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // MongoDB istemcisini ve veritabanını bağlantı ayarlarıyla oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            // Veritabanındaki ürün detay koleksiyonunu alır.
            _productDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);

            // AutoMapper'ı enjekte eder.
            _mapper = mapper;
        }

        // Yeni bir ürün detayı oluşturmayı sağlayan metot.
        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            // DTO'yu ürün detay entity'sine dönüştürür.
            var value = _mapper.Map<ProductDetail>(createProductDetailDto);

            // MongoDB koleksiyonuna yeni bir belge ekler.
            await _productDetailCollection.InsertOneAsync(value);
        }

        // Bir ürün detayını silmeyi sağlayan metot.
        public async Task DeleteProductDetailAsync(string id)
        {
            // Verilen ID'ye göre ürün detayını MongoDB'den siler.
            await _productDetailCollection.DeleteOneAsync(x => x.ProductDetailID == id);
        }

        // Tüm ürün detaylarını getiren metot.
        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            // Tüm ürün detaylarını MongoDB'den alır.
            var values = await _productDetailCollection.Find(x => true).ToListAsync();

            // Entity'leri DTO'ya dönüştürür.
            return _mapper.Map<List<ResultProductDetailDto>>(values);
        }

        // Belirli bir ID'ye göre ürün detayını getiren metot.
        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            // Verilen ID'ye göre ürün detayını MongoDB'den alır.
            var values = await _productDetailCollection.Find<ProductDetail>(x => x.ProductDetailID == id).FirstOrDefaultAsync();

            // Entity'yi DTO'ya dönüştürür ve döner.
            return _mapper.Map<GetByIdProductDetailDto>(values);
        }

        // Ürün detayını güncellemeyi sağlayan metot.
        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            // DTO'yu ürün detay entity'sine dönüştürür.
            var values = _mapper.Map<ProductDetail>(updateProductDetailDto);

            // MongoDB koleksiyonunda verilen ID'ye sahip ürünü günceller.
            await _productDetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailID == updateProductDetailDto.ProductDetailID, values);
        }
    }
}
