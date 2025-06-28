using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMapper _mapper;

        // ProductImageService sınıfının kurucu metodudur.
        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // MongoDB istemcisini ve veritabanını bağlantı ayarlarıyla oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            // Veritabanındaki ürün resmi koleksiyonunu alır.
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);

            // AutoMapper'ı enjekte eder.
            _mapper = mapper;
        }

        // Yeni bir ürün resmi oluşturmayı sağlayan metot.
        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            // DTO'yu ürün resmi entity'sine dönüştürür.
            var values = _mapper.Map<ProductImage>(createProductImageDto);

            // MongoDB koleksiyonuna yeni bir belge ekler.
            await _productImageCollection.InsertOneAsync(values);
        }

        // Bir ürün resmini silmeyi sağlayan metot.
        public async Task DeleteProductImageAsync(string id)
        {
            // Verilen ID'ye göre ürün resmini MongoDB'den siler.
            await _productImageCollection.DeleteOneAsync(x => x.ProductImageID == id);
        }

        // Tüm ürün resimlerini getiren metot.
        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            // Tüm ürün resimlerini MongoDB'den alır.
            var values = await _productImageCollection.Find(x => true).ToListAsync();

            // Entity'leri DTO'ya dönüştürür.
            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        // Belirli bir ID'ye göre ürün resmi getiren metot.
        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            // Verilen ID'ye göre ürün resmini MongoDB'den alır.
            var values = await _productImageCollection.Find<ProductImage>(x => x.ProductImageID == id).FirstOrDefaultAsync();

            // Entity'yi DTO'ya dönüştürür.
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        // Bir ürün resmini güncellemeyi sağlayan metot.
        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            // DTO'yu ürün resmi entity'sine dönüştürür.
            var values = _mapper.Map<ProductImage>(updateProductImageDto);

            // Verilen ID'ye göre ürün resmini MongoDB'de günceller.
            await _productImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, values);
        }
    }
}

// Bu sınıf ve metotlar, bir MongoDB veritabanı üzerinde işlem yaparak ürün resimleriyle ilgili CRUD (Oluşturma, Okuma, Güncelleme, Silme) işlemlerini gerçekleştirir. AutoMapper kullanarak DTO (Data Transfer Object) ve entity (varlık) arasında veri dönüşümü sağlar. Örneğin, bir ürün resmi oluşturulabilir, güncellenebilir, silinebilir veya tüm resimler alınabilir.