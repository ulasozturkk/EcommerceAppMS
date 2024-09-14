using EcommerceAppMS.Shared.Services;
using ECommerceAppMS.Basket.Services;
using ECommerceAppMS.Basket.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options => {
  Options.Authority = builder.Configuration["IdentityServerURL"];
  Options.Audience = "resource_basket";
  Options.RequireHttpsMetadata = false;
});

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddSingleton<RedisService>(sp => {
  var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
  var redis = new RedisService(redisSettings.Host, redisSettings.Port);
  redis.Connect();
  return redis;
});
builder.Services.AddControllers(opt => {
  opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

builder.Services.AddSwaggerGen(o => {
  o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ECommerceAppMS.Services.Basket", Version = "v1" });
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();