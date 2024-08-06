﻿using Banking.Application.Interfaces;
using Banking.Domain.Entities.Customers;
using Banking.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Custormer custormer)
        {
            _dbContext.Customers.Add(custormer.Convert());

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Custormer?> GetByIdAsync(Guid id, bool includeTransactions)
        {
            var dbCustomer = await _dbContext.Customers.FindAsync(id);

            return dbCustomer?.Convert();
        }
    }
}