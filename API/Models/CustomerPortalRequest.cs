using System.ComponentModel.DataAnnotations;

namespace API.Models
{
	public class CustomerPortalRequest
	{
		[Required]
		public string ReturnUrl { get; set; }
	}
}