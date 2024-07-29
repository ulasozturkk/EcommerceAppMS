using Microsoft.AspNetCore.Identity;

namespace ECommerceAppMS.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    public string City {  get; set; }
    }
}
