using EcommerceAppMS.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAppMS.Shared.ControllerBases;

public class CustomBaseController : ControllerBase {

  public IActionResult CreateActionResultInstance<T>(ResponseDTO<T> response) {
    return new ObjectResult(response) {
      StatusCode = response.StatusCode
    };
  }
}