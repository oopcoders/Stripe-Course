using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentsController : ControllerBase
	{
		private readonly StripeSettings _stripeSettings;
		public PaymentsController(IOptions<StripeSettings> stripeSettings)
		{
			_stripeSettings = stripeSettings.Value;
		}

		[HttpPost("create-checkout-session")]
		public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
		{
			var options = new SessionCreateOptions
			{
				SuccessUrl = req.SuccessUrl,
				CancelUrl = req.FailureUrl,
				PaymentMethodTypes = new List<string>
				{
					"card",
				},
				Mode = "subscription",
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						Price = req.PriceId,
						Quantity = 1,
					},
				},
			};

			var service = new SessionService();
			service.Create(options);
			try
			{
				var session = await service.CreateAsync(options);
				return Ok(new CreateCheckoutSessionResponse
				{
					SessionId = session.Id,
					PublicKey = _stripeSettings.PublicKey
				});
			}
			catch (StripeException e)
			{
				Console.WriteLine(e.StripeError.Message);
				return BadRequest(new ErrorResponse
				{
					ErrorMessage = new ErrorMessage
					{
						Message = e.StripeError.Message,
					}
				});
			}
		}

		[HttpPost("customer-portal")]
		public IActionResult CustomerPortal([FromBody] CustomerPortalRequest req)
		{
			//Snippet Goes here
			return Ok();
		}
	}


}
