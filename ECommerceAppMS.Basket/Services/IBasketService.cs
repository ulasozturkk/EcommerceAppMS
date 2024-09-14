using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.Basket.Dtos;

namespace ECommerceAppMS.Basket.Services;

public interface IBasketService {

  Task<ResponseDTO<BasketDto>> GetBasket(string userId);

  Task<ResponseDTO<bool>> SaveOrUpdate(BasketDto basketDto);

  Task<ResponseDTO<bool>> Delete(string userId);
}