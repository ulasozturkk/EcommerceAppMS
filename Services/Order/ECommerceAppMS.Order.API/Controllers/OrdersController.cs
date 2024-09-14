using ECommerceAppMS.Services.Order.Application.Commands;
using ECommerceAppMS.Services.Order.Application.Queries;
using EcommerceAppMS.Shared.ControllerBases;
using EcommerceAppMS.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAppMS.Order.API.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : CustomBaseController {
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService) {
      _mediator = mediator;
      _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders() {
      var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserID });

      return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand) {
      var response = await _mediator.Send(createOrderCommand);

      return CreateActionResultInstance(response);
    }
  }
}
