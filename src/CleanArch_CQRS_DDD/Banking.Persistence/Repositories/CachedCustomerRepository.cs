using Banking.Application.Interfaces;
using Banking.Domain.Entities.Customers;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Banking.Persistence.Repositories
{
    internal class CachedCustomerRepository : ICustomerRepository
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDistributedCache _distributedCache;

        public CachedCustomerRepository(
            ICustomerRepository customerRepository,
            IDistributedCache distributedCache)
        {
            _customerRepository = customerRepository;
            _distributedCache = distributedCache;
        }

        public async Task AddAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            var value = await _distributedCache.GetAsync(id.ToString());

            if (value is not null)
            {
                return JsonSerializer.Deserialize<Customer>(Encoding.UTF8.GetString(value));
            }

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is not null)
            {
                var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddHours(24))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                await _distributedCache
                    .SetAsync(id.ToString(),
                        JsonSerializer.SerializeToUtf8Bytes(customer), options);
            }

            return customer;
        }
    }
}