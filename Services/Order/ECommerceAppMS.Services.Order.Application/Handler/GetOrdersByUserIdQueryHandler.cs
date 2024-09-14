using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.Services.Order.Application.Dtos;
using ECommerceAppMS.Services.Order.Application.Mapping;
using ECommerceAppMS.Services.Order.Application.Queries;
using ECommerceAppMS.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAppMS.Services.Order.Application.Handler;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, ResponseDTO<List<OrderDto>>> {
  private readonly OrderDbContext _ctx;

  public GetOrdersByUserIdQueryHandler(OrderDbContext ctx) {
    _ctx = ctx;
  }

  public async Task<ResponseDTO<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken) {
    var orders = await _ctx.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

    if (!orders.Any()) {
      return ResponseDTO<List<OrderDto>>.Success(new List<OrderDto>(), 200);
    }

    var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

    return ResponseDTO<List<OrderDto>>.Success(ordersDto, 200);
  }
}