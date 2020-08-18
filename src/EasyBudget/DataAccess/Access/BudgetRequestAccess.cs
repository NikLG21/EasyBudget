using System;
using System.Collections.Generic;
using System.Linq;
using EasyBudget.Common.DataAccess.Commands;
using EasyBudget.Common.Model;

namespace DataAccess
{
    public class BudgetRequestAccess : IBudgetRequestAccess
    {
        public void AddBudgetRequest(BudgetRequest request)
        {
            throw new NotImplementedException();
        }

        public void UpdateBudgetRequest(BudgetRequest request)
        {
            throw new NotImplementedException();
        }

        public void DeleteBudgetRequest(Guid budgetRequestId)
        {
            throw new NotImplementedException();
        }

        public BudgetRequest GetBudgetRequest(Guid budgetRequestId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                return context.BudgetRequests.FirstOrDefault(e => e.Id == budgetRequestId);
            }
        }

        public List<BudgetRequest> GetBudgetRequestByOriginatorList(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<BudgetRequest> GetBudgetRequestByApproverList(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
