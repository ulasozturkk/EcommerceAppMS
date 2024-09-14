using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Catalog.Services;
using EcommerceAppMS.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAppMS.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : CustomBaseController {
  private readonly ICourseService _courseService;

  public CourseController(ICourseService courseService) {
    _courseService = courseService;
  }

  [HttpGet("GetAll")]
  public async Task<IActionResult> GetAll() {
    var response = await _courseService.GetAllAsync();
    return CreateActionResultInstance(response);
  }

  [HttpGet("GetByID")]
  public async Task<IActionResult> GetById(string id) {
    var response = await _courseService.GetByIDAsync(id);
    return CreateActionResultInstance(response);
  }

  [HttpGet("GetByUserID")]
  public async Task<IActionResult> GetAllByUserId(string UserId) {
    var response = await _courseService.GetAllByUserIDAsync(UserId);
    return CreateActionResultInstance(response);
  }

  [HttpPost]
  public async Task<IActionResult> Create(CourseCreateDto courseCreateDto) {
    var response = await _courseService.CreateAsync(courseCreateDto);
    return CreateActionResultInstance(response);
  }

  [HttpPut]
  public async Task<IActionResult> Update(UpdateCourseDto updateCourseDto) {
    var response = await _courseService.UpdateAsync(updateCourseDto);
    return CreateActionResultInstance(response);
  }

  [HttpDelete]
  public async Task<IActionResult> Delete(string id) {
    var response = await _courseService.DeleteAsync(id);
    return CreateActionResultInstance(response);
  }
}