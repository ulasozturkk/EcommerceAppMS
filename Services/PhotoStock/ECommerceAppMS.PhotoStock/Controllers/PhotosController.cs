using EcommerceAppMS.Shared.ControllerBases;
using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.PhotoStock.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAppMS.PhotoStock.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PhotosController : CustomBaseController {

  [HttpPost]
  public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken) {
    if (photo != null && photo.Length > 0) {
      var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

      using (var stream = new FileStream(path, FileMode.Create)) {
        await photo.CopyToAsync(stream, cancellationToken);
      }

      //http://www.photostock.api.com/photos/asdasdf.jpeg   gibi olacak
      var returnPath = "photos/" + photo.FileName;

      PhotoDto photoDto = new() { Url = returnPath };

      return CreateActionResultInstance(ResponseDTO<PhotoDto>.Success(photoDto, 201));
    }

    return CreateActionResultInstance(ResponseDTO<PhotoDto>.Fail("photo is empty", 404));
  }

  [HttpGet]
  public IActionResult PhotoDelete(string photoURL) {
    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoURL);
    if (System.IO.File.Exists(path)) {
      return CreateActionResultInstance(ResponseDTO<NoDataDTO>.Fail("photo not found", 404));
    }

    System.IO.File.Delete(path);
    return CreateActionResultInstance(ResponseDTO<NoDataDTO>.Success(204));
  }
}