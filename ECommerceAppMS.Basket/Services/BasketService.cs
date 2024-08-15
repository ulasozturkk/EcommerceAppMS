using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.Basket.Dtos;
using System.Text.Json;

namespace ECommerceAppMS.Basket.Services;

public class BasketService : IBasketService {

   private readonly RedisService _redisService;

 public BasketService(RedisService redisService) {
    _redisService = redisService;
  }
  public async Task<ResponseDTO<bool>> Delete(string userId) {

    var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);
    return status ? ResponseDTO<bool>.Success(204) : ResponseDTO<bool>.Fail("Basket not found", 404);
  }

  public async Task<ResponseDTO<BasketDto>> GetBasket(string userId) {

    var existBasket = await _redisService.GetDatabase().StringGetAsync(userId);
    if (String.IsNullOrEmpty(existBasket)) {
      return ResponseDTO<BasketDto>.Fail("Basket not found", 404);

    }

    return ResponseDTO<BasketDto>.Success(data: JsonSerializer.Deserialize<BasketDto>(existBasket), 200);

  }

  public async Task<ResponseDTO<bool>> SaveOrUpdate(BasketDto basketDto) {
    var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
    return status ? ResponseDTO<bool>.Success(204) : ResponseDTO<bool>.Fail("Basket could not update or save", 500);
  }
}
