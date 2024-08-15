namespace ECommerceAppMS.Basket.Dtos {
  public class BasketDto {
        public string? UserId { get; set; }
        public string? DiscountCode { get; set; }
        public List<BasketItemDto> basketItems { get; set; }

        public decimal TotalPrice {
      get => (decimal)basketItems.Sum(x => x.Price * x.Queantity);

      }
    }
}
