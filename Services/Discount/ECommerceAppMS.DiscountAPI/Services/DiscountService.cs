using Dapper;
using EcommerceAppMS.Shared.Dtos;
using ECommerceAppMS.DiscountAPI.Models;
using Npgsql;
using System.Data;

namespace ECommerceAppMS.DiscountAPI.Services;

public class DiscountService : IDiscountService {
  private readonly IConfiguration _configuration;
  private readonly IDbConnection _dbConnection;

  public DiscountService(IConfiguration configuration, IDbConnection dbConnection) {
    _configuration = configuration;
    _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
  }

  public async Task<ResponseDTO<NoDataDTO>> Delete(int id) {
    var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

    return status > 0 ? ResponseDTO<NoDataDTO>.Success(204) : ResponseDTO<NoDataDTO>.Fail("discount not found", 404);
  }

  public async Task<ResponseDTO<List<Discount>>> GetAll() {
    var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");

    return ResponseDTO<List<Discount>>.Success(discounts.ToList(), 200);
  }

  public async Task<ResponseDTO<Discount>> GetByCodeAndUserId(string code, string userid) {
    var discount = await _dbConnection.QueryAsync<Discount>("select * from discount where userid=@UserId and code=@Code", new {
      userid = userid,
      code = code
    });

    var hasDiscount = discount.FirstOrDefault();
    if (hasDiscount == null) {
      return ResponseDTO<Discount>.Success(hasDiscount, 200);
    } else {
      return ResponseDTO<Discount>.Fail("discount not found", 404);
    }
  }

  public async Task<ResponseDTO<Discount>> GetById(int id) {
    var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@id", new { Id = id })).SingleOrDefault();
    if (discount == null) {
      return ResponseDTO<Discount>.Fail("Discount not found", 404);
    }
    return ResponseDTO<Discount>.Success(discount, 200);
  }

  public async Task<ResponseDTO<NoDataDTO>> Save(Discount discount) {
    var status = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
    if (status > 0) {
      return ResponseDTO<NoDataDTO>.Success(204);
    } else {
      return ResponseDTO<NoDataDTO>.Fail("an error accured while adding", 500);
    }
  }

  public async Task<ResponseDTO<NoDataDTO>> Update(Discount discount) {
    var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Rate = discount.Rate });
    if (status > 0) {
      return ResponseDTO<NoDataDTO>.Success(204);
    } else {
      return ResponseDTO<NoDataDTO>.Fail("discount not found", 404);
    }
  }
}