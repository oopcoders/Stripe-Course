using System;

namespace API.Data.Entities
{
	public class Subscriber
	{
		public string Id { get; set; }
		public string CustomerId { get; set; }
		public string Status { get; set; }
		public DateTime CurrentPeriodEnd { get; set; }


	}
}