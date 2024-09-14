using AutoMapper;
using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Catalog.Models;
using EcommerceAppMS.Catalog.Settings;
using EcommerceAppMS.Shared.Dtos;
using MongoDB.Driver;

namespace EcommerceAppMS.Catalog.Services;

public class CategoryService : ICategoryService {
  private readonly IMongoCollection<Category> _categoryCollection;
  private readonly IMapper _mapper;

  public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings) {
    var client = new MongoClient(databaseSettings.ConnectionString);
    var database = client.GetDatabase(databaseSettings.DatabaseName);
    _mapper = mapper;
    _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
  }

  public async Task<ResponseDTO<List<CategoryDto>>> GetAllAsync() {
    var categorites = await _categoryCollection.Find(category => true).ToListAsync();
    return ResponseDTO<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categorites), 200);
  }

  public async Task<ResponseDTO<CategoryDto>> CreateAsync(CategoryDto categoryDto) {
    var category = _mapper.Map<Category>(categoryDto);
    await _categoryCollection.InsertOneAsync(category);
    return ResponseDTO<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
  }

  public async Task<ResponseDTO<CategoryDto>> GetByIDAsync(string id) {
    var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
    if (category == null) {
      return ResponseDTO<CategoryDto>.Fail("category not found", 404);
    }
    return ResponseDTO<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
  }
}