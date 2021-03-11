using System;
using API.Data.Entities;

namespace API.Services
{
	public interface IJWTTokenGenerator
	{
		string GenerateToken(User user, DateTime expDate, bool isSubscriber);
	}
}