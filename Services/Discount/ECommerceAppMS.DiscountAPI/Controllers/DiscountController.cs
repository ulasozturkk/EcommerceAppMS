using EcommerceAppMS.Shared.ControllerBases;
using EcommerceAppMS.Shared.Services;
using ECommerceAppMS.DiscountAPI.Models;
using ECommerceAppMS.DiscountAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAppMS.DiscountAPI.Controllers; 
[Route("api/[controller]")]
[ApiController]
public class DiscountController : CustomBaseController {

  private readonly IDiscountService discountService;
  private readonly ISharedIdentityService sharedIdentityService;

  public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService) {
    this.discountService = discountService;
    this.sharedIdentityService = sharedIdentityService;
  }


  [HttpGet]
  public async Task<IActionResult> GetAll() {
    return CreateActionResultInstance(await discountService.GetAll());
  }

  //api/discounts/4
  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id) {
    return CreateActionResultInstance(await discountService.GetById(id));
  }

  [Route("/api/[controller]/[action]/{code}")]
  [HttpGet]
  public async Task<IActionResult> GetByCode(string Code) {
    var userId = sharedIdentityService.GetUserID;

    var discount = await discountService.GetByCodeAndUserId(Code, userId);
    return CreateActionResultInstance(discount);


  }

  [HttpPost]
  public async Task<IActionResult> Save(Discount discount) {

    return CreateActionResultInstance(await discountService.Save(discount));
  }
  [HttpPut]
  public async Task<IActionResult> Update(Discount discount) {

    return CreateActionResultInstance(await discountService.Update(discount));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id) {

    return CreateActionResultInstance(await discountService.Delete(id));
  }


}
