using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Shared.Dtos;

namespace EcommerceAppMS.Catalog.Services;

public interface ICategoryService {

  Task<ResponseDTO<List<CategoryDto>>> GetAllAsync();

  Task<ResponseDTO<CategoryDto>> CreateAsync(CategoryDto category);

  Task<ResponseDTO<CategoryDto>> GetByIDAsync(string id);
}