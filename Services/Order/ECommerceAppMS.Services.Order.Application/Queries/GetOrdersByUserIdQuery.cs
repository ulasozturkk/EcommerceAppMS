using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.Services.Order.Application.Dtos;
using MediatR;

namespace ECommerceAppMS.Services.Order.Application.Queries {

  public class GetOrdersByUserIdQuery : IRequest<ResponseDTO<List<OrderDto>>> {
    public string UserId { get; set; }
  }
}