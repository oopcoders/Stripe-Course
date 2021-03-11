using API.Models;
using Microsoft.AspNetCore.Mvc;
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
		// POST api/<PaymentsController> Create Session
		[HttpPost("create-checkout-session")]
		public IActionResult CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
		{
			//Snippet Goes here
			return Ok();
		}

		[HttpPost("customer-portal")]
		public IActionResult CustomerPortal([FromBody] CustomerPortalRequest req)
		{
			//Snippet Goes here
			return Ok();
		}
	}


}
