﻿using EcommerceAppMS.Shared.ControllerBases;
using EcommerceAppMS.Shared.Services;
using ECommerceAppMS.Basket.Dtos;
using ECommerceAppMS.Basket.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAppMS.Basket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : CustomBaseController {
  private readonly IBasketService _basketService;
  private readonly ISharedIdentityService _sharedIdentityService;

  private BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService) {
    _basketService = basketService;
    _sharedIdentityService = sharedIdentityService;
  }

  [HttpGet]
  public async Task<IActionResult> GetBasket() {
    return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserID));
  }

  [HttpPost]
  public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto) {
    var response = await _basketService.SaveOrUpdate(basketDto);
    return CreateActionResultInstance(response);
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteBasket() {
    var response = await _basketService.Delete(_sharedIdentityService.GetUserID);
    return CreateActionResultInstance(response);
  }
}