using EcommerceAppMS.Shared.Dtos;

namespace ECommerceAppMS.DiscountAPI.Services; 

public interface IDiscountService {

  Task<ResponseDTO<List<Models.Discount>>> GetAll();
  Task<ResponseDTO<Models.Discount>> GetById(int id);
  Task<ResponseDTO<NoDataDTO>> Save(Models.Discount discount);  
  Task<ResponseDTO<NoDataDTO>> Update(Models.Discount discount);
  Task<ResponseDTO<NoDataDTO>> Delete(int id);
  Task<ResponseDTO<Models.Discount>> GetByCodeAndUserId(string code,string userid);

}
