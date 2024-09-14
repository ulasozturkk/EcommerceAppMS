using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.Services.Order.Application.Commands;
using ECommerceAppMS.Services.Order.Application.Dtos;
using ECommerceAppMS.Services.Order.Domain.OrderAggregate;
using ECommerceAppMS.Services.Order.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAppMS.Services.Order.Application.Handler;


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDTO<CreatedOrderDto>> {

  private readonly OrderDbContext _context;

  public CreateOrderCommandHandler(OrderDbContext context) {
    _context = context;
  }
  public async Task<ResponseDTO<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken) {
    var newAddress = new Adress(request.AddressDto.Province, request.AddressDto.District, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);

    Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

    request.OrderItems.ForEach(x =>
    {
      newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
    });

    await _context.Orders.AddAsync(newOrder);

    await _context.SaveChangesAsync();

    return ResponseDTO<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
  }
}