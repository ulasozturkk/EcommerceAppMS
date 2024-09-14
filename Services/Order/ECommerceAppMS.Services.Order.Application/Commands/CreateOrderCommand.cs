using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.Services.Order.Application.Dtos;
using MediatR;

namespace ECommerceAppMS.Services.Order.Application.Commands {

  public class CreateOrderCommand : IRequest<EcommerceAppMS.Shared.Dtos.ResponseDTO<CreatedOrderDto>> {
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto AddressDto { get; set; }
  }
}