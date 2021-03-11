using API.Data.Entities;
using API.Data.Repositories;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentsController : ControllerBase
	{
		private readonly StripeSettings _stripeSettings;
		private readonly UserManager<User> _userManager;
		private readonly ISubscriptionRepository _subscriberRepository;
		public PaymentsController(IOptions<StripeSettings> stripeSettings, UserManager<User> userManager, ISubscriptionRepository subscriberRepository)
		{
			_subscriberRepository = subscriberRepository;
			_stripeSettings = stripeSettings.Value;
			_userManager = userManager;
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

		[Authorize]
		[HttpPost("customer-portal")]
		public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalRequest req)
		{

			try
			{
				ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
				var claim = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
				var userFromDb = await _userManager.FindByNameAsync(claim.Value);

				if (userFromDb == null)
				{
					return BadRequest();
				}
				var options = new Stripe.BillingPortal.SessionCreateOptions
				{
					Customer = userFromDb.CustomerId,
					ReturnUrl = req.ReturnUrl,
				};
				var service = new Stripe.BillingPortal.SessionService();
				var session = await service.CreateAsync(options);

				return Ok(new
				{
					url = session.Url
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

		// POST api/<PaymentsController>/webhook
		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(
				 json,
				 Request.Headers["Stripe-Signature"],
				 _stripeSettings.WHSecret
			   );

				// Handle the event
				if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
				{
					var subscription = stripeEvent.Data.Object as Subscription;
					//Do stuff
					await addSubscriptionToDb(subscription);
				}
				else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
				{
					var session = stripeEvent.Data.Object as Stripe.Subscription;

					// Update Subsription
					await updateSubscription(session);
				}
				else if (stripeEvent.Type == Events.CustomerCreated)
				{
					var customer = stripeEvent.Data.Object as Customer;
					//Do Stuff
					await addCustomerIdToUser(customer);
				}
				// ... handle other event types
				else
				{
					// Unexpected event type
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}
				return Ok();
			}
			catch (StripeException e)
			{
				Console.WriteLine(e.StripeError.Message);
				return BadRequest();
			}
		}

		private async Task updateSubscription(Subscription subscription)
		{
			try
			{
				var subscriptionFromDb = await _subscriberRepository.GetByIdAsync(subscription.Id);
				if (subscriptionFromDb != null)
				{
					subscriptionFromDb.Status = subscription.Status;
					subscriptionFromDb.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
					await _subscriberRepository.UpdateAsync(subscriptionFromDb);
					Console.WriteLine("Subscription Updated");
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);

				Console.WriteLine("Unable to update subscription");

			}

		}

		private async Task addCustomerIdToUser(Customer customer)
		{
			try
			{
				var userFromDb = await _userManager.FindByEmailAsync(customer.Email);

				if (userFromDb != null)
				{
					userFromDb.CustomerId = customer.Id;
					await _userManager.UpdateAsync(userFromDb);
					Console.WriteLine("Customer Id added to user ");
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine("Unable to add customer id to user");
				Console.WriteLine(ex);
			}
		}

		private async Task addSubscriptionToDb(Subscription subscription)
		{
			try
			{
				var subscriber = new Subscriber
				{
					Id = subscription.Id,
					CustomerId = subscription.CustomerId,
					Status = "active",
					CurrentPeriodEnd = subscription.CurrentPeriodEnd
				};
				await _subscriberRepository.CreateAsync(subscriber);

				//You can send the new subscriber an email welcoming the new subscriber
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("Unable to add new subscriber to Database");
				Console.WriteLine(ex.Message);
			}
		}
	}


}
