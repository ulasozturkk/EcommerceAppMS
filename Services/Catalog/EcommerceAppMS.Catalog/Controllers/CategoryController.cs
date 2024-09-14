using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Catalog.Services;
using EcommerceAppMS.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAppMS.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : CustomBaseController {
  private readonly ICategoryService _categoryService;

  public CategoryController(ICategoryService categoryService) {
    _categoryService = categoryService;
  }

  [HttpGet("GetAll")]
  public async Task<IActionResult> GetAll() {
    var categories = await _categoryService.GetAllAsync();
    return CreateActionResultInstance(categories);
  }

  [HttpGet("GetById")]
  public async Task<IActionResult> GetById(string id) {
    var categories = await _categoryService.GetByIDAsync(id);
    return CreateActionResultInstance(categories);
  }

  [HttpPost]
  public async Task<IActionResult> Create(CategoryDto categoryDto) {
    var categories = await _categoryService.CreateAsync(categoryDto);
    return CreateActionResultInstance(categories);
  }
}