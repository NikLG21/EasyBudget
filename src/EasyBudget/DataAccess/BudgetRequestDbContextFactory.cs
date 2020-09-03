using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class BudgetRequestDbContextFactory : IBudgetRequestDbContextFactory
    {
        private IConfiguration _configuration;

        public BudgetRequestDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BudgetRequestDbContext Create()
        {
            string connectionString = _configuration["BudgetRequestDbContext"];
            return new BudgetRequestDbContext(connectionString);
        }
    }
}
