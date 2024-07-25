using Core.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Custormer> GetByIdAsync(Guid id, bool includeTransactions);

        void Add(Custormer custormer);
    }
}