namespace ECommerceAppMS.Services.Order.Application.Dtos {

  public class OrderDto {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public AddressDto Adress { get; set; }
    public string BuyerId { get; set; }

    public List<OrderItemDto> _orderItems { get; set; }
  }
}