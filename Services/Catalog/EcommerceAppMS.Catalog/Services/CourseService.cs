using AutoMapper;
using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Catalog.Models;
using EcommerceAppMS.Catalog.Settings;
using EcommerceAppMS.Shared.Dtos;
using MongoDB.Driver;

namespace EcommerceAppMS.Catalog.Services;

public class CourseService : ICourseService {
  private readonly IMongoCollection<Course> _courseCollection;
  private readonly IMongoCollection<Category> _categoryCollection;
  private readonly IMapper _mapper;

  public CourseService(IMapper mapper, IDatabaseSettings databaseSettings) {
    _mapper = mapper;
    var client = new MongoClient(databaseSettings.ConnectionString);
    var database = client.GetDatabase(databaseSettings.CourseCollectionName);
    _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
    _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
  }

  public async Task<ResponseDTO<List<CourseDto>>> GetAllAsync() {
    var courses = await _courseCollection.Find(c => true).ToListAsync();
    if (courses.Any()) {
      foreach (var course in courses) {
        course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
      }
    } else {
      courses = new List<Course>();
    }
    return ResponseDTO<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
  }

  public async Task<ResponseDTO<CourseDto>> GetByIDAsync(string id) {
    var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
    if (course == null) {
      return ResponseDTO<CourseDto>.Fail("course not found", 404);
    }
    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

    return ResponseDTO<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
  }

  public async Task<ResponseDTO<List<CourseDto>>> GetAllByUserIDAsync(string userID) {
    var courses = await _courseCollection.Find<Course>(x => x.UserId == userID).ToListAsync();
    if (courses.Any()) {
      foreach (var course in courses) {
        course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
      }
    } else {
      courses = new List<Course>();
    }
    return ResponseDTO<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
  }

  public async Task<ResponseDTO<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto) {
    var newCourse = _mapper.Map<Course>(courseCreateDto);
    newCourse.CreatedTime = DateTime.Now;
    await _courseCollection.InsertOneAsync(newCourse);

    return ResponseDTO<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 201);
  }

  public async Task<ResponseDTO<NoDataDTO>> UpdateAsync(UpdateCourseDto updateCourseDto) {
    var update = _mapper.Map<Course>(updateCourseDto);

    var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourseDto.Id, update);

    if (result == null) {
      return ResponseDTO<NoDataDTO>.Fail("Course not found", 404);
    }

    return ResponseDTO<NoDataDTO>.Success(204);
  }

  public async Task<ResponseDTO<NoDataDTO>> DeleteAsync(string id) {
    var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
    if (result.DeletedCount > 0) {
      return ResponseDTO<NoDataDTO>.Success(204);
    } else {
      return ResponseDTO<NoDataDTO>.Fail("course not found", 404);
    }
  }
}