using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Entities;

namespace API.Data.Repositories
{
	public interface ISubscriptionRepository
	{
		Task<Subscriber> UpdateAsync(Subscriber subscription);
		Task<IEnumerable<Subscriber>> GetAsync();
		Task<Subscriber> GetByIdAsync(string id);
		Task<Subscriber> GetByCustomerIdAsync(string id);
		Task<Subscriber> CreateAsync(Subscriber subscription);
		Task DeleteAsync(Subscriber subscription);

	}
}