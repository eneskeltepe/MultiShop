using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;

        // ProductService sınıfının kurucu metodudur.
        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // MongoDB istemcisini ve veritabanını bağlantı ayarlarıyla oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            // Veritabanındaki ürün koleksiyonunu alır.
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);

            // AutoMapper'ı enjekte eder.
            _mapper = mapper;
        }

        // Yeni bir ürün oluşturmayı sağlayan metot.
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            // DTO'yu ürün entity'sine dönüştürür.
            var values = _mapper.Map<Product>(createProductDto);

            // MongoDB koleksiyonuna yeni bir belge ekler.
            await _productCollection.InsertOneAsync(values);
        }

        // Bir ürünü silmeyi sağlayan metot.
        public async Task DeleteProductAsync(string id)
        {
            // Verilen ID'ye göre ürünü MongoDB'den siler.
            await _productCollection.DeleteOneAsync(x => x.ProductID == id);
        }

        // Tüm ürünleri getiren metot.
        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            // Tüm ürünleri MongoDB'den alır.
            var values = await _productCollection.Find(x => true).ToListAsync();

            // Entity'leri DTO'ya dönüştürür.
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        // Belirli bir ID'ye göre ürünü getiren metot.
        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            // Verilen ID'ye göre ürünü MongoDB'den alır.
            var values = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();

            // Alınan ürünü DTO'ya dönüştürür ve döner.
            return _mapper.Map<GetByIdProductDto>(values);
        }

        // Ürünü güncellemeyi sağlayan metot.
        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            // Güncelleme DTO'sunu ürün entity'sine dönüştürür.
            var values = _mapper.Map<Product>(updateProductDto);

            // Verilen ID'ye göre ürünü MongoDB'de günceller.
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID, values);
        }
    }
}
