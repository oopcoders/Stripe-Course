using Microsoft.AspNetCore.Identity;

namespace API.Data.Entities
{
	public class User : IdentityUser
	{
		public string CustomerId { get; set; }
	}
}