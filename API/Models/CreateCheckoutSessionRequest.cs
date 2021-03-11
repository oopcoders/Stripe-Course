using System.ComponentModel.DataAnnotations;

namespace API.Models
{
	public class CreateCheckoutSessionRequest
	{
		[Required]
		public string PriceId { get; set; }
		[Required]
		public string SuccessUrl { get; set; }
		[Required]
		public string FailureUrl { get; set; }
	}
}