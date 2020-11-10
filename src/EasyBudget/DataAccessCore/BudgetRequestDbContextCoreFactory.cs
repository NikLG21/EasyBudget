using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessCore
{
    public class BudgetRequestDbContextCoreFactory : IBudgetRequestDbContextCoreFactory
    {
        private readonly IConfiguration _configuration;

        public BudgetRequestDbContextCoreFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BudgetRequestDbContextCore Create()
        {
            string connectionString = _configuration["BudgetRequestDbContext"];
            return new BudgetRequestDbContextCore(connectionString);
        }
    }
}
