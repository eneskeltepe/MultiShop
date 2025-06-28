using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        // CategoryService sınıfının kurucu metodudur.
        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // MongoDB istemcisini ve veritabanını bağlantı ayarlarıyla oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            // Veritabanındaki kategori koleksiyonunu alır.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);

            // AutoMapper'ı enjekte eder.
            _mapper = mapper;
        }

        // Yeni bir kategori oluşturmayı sağlayan metot.
        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            // DTO'yu kategori entity'sine dönüştürür.
            var value = _mapper.Map<Category>(createCategoryDto);

            // MongoDB koleksiyonuna yeni bir belge ekler.
            await _categoryCollection.InsertOneAsync(value);
        }

        // Bir kategoriyi silmeyi sağlayan metot.
        public async Task DeleteCategoryAsync(string id)
        {
            // Verilen ID'ye göre kategoriyi MongoDB'den siler.
            await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id);
        }

        // Tüm kategorileri getiren metot.
        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            // Tüm kategorileri MongoDB'den alır.
            var values = await _categoryCollection.Find(x => true).ToListAsync();

            // Entity'leri DTO'ya dönüştürür.
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        // Belirli bir ID'ye göre kategoriyi getiren metot.
        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            // Verilen ID'ye göre kategoriyi MongoDB'den alır.
            var values = await _categoryCollection.Find<Category>(x => x.CategoryID == id).FirstOrDefaultAsync();

            // Entity'yi DTO'ya dönüştürür.
            return _mapper.Map<GetByIdCategoryDto>(values);
        }

        // Kategoriyi güncellemeyi sağlayan metot.
        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            // DTO'yu kategori entity'sine dönüştürür.
            var values = _mapper.Map<Category>(updateCategoryDto);

            // MongoDB koleksiyonunda verilen ID'ye sahip kategoriyi günceller.
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryID == updateCategoryDto.CategoryID, values);
        }
    }
}
