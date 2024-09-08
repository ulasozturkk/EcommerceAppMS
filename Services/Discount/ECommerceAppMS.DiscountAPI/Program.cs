using EcommerceAppMS.Shared.Services;
using ECommerceAppMS.DiscountAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options => {
  Options.Authority = builder.Configuration["IdentityServerURL"];
  Options.Audience = "resource_discount";
  Options.RequireHttpsMetadata = false;

});
builder.Services.AddControllers(opt => {
  opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

builder.Services.AddSwaggerGen(o => {
  o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ECommerceAppMS.Services.discount", Version = "v1" });
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
