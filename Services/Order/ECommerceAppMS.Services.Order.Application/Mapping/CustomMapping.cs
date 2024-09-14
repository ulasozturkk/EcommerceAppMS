using AutoMapper;
using ECommerceAppMS.Services.Order.Application.Dtos;
using ECommerceAppMS.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAppMS.Services.Order.Application.Mapping {
  public class CustomMapping:Profile {

    public CustomMapping() {
      CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
      CreateMap<OrderItem, OrderItemDto>().ReverseMap();
      CreateMap<Adress, AddressDto>().ReverseMap();

    }
  }
}
