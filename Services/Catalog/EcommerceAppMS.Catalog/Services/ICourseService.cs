using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Shared.Dtos;

namespace EcommerceAppMS.Catalog.Services;

public interface ICourseService {

  Task<ResponseDTO<List<CourseDto>>> GetAllAsync();

  Task<ResponseDTO<CourseDto>> GetByIDAsync(string id);

  Task<ResponseDTO<List<CourseDto>>> GetAllByUserIDAsync(string userID);

  Task<ResponseDTO<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);

  Task<ResponseDTO<NoDataDTO>> UpdateAsync(UpdateCourseDto updateCourseDto);

  Task<ResponseDTO<NoDataDTO>> DeleteAsync(string id);
}