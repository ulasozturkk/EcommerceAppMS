﻿using ECommerceAppMS.Services.Order.Domain.Core;

namespace ECommerceAppMS.Services.Order.Domain.OrderAggregate {

  public class Order : Entity, IAggregateRoot {
    public DateTime CreatedDate { get; private set; }
    public Adress Adress { get; private set; }
    public string BuyerId { get; private set; }

    private readonly List<OrderItem> _orderItems;

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Order() { }

    public Order(string buyerId, Adress adress) {
      _orderItems = new List<OrderItem>();
      CreatedDate = DateTime.Now;
      BuyerId = buyerId;
      Adress = adress;
    }

    public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl) {
      var existProduct = _orderItems.Any(x => x.ProductId == productId);
      if (existProduct == false) {
        var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
        _orderItems.Add(newOrderItem);
      }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
  }
}