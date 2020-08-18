using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EasyBudget.Common.DataAccess.Commands;
using EasyBudget.Common.Model;

namespace DataAccess
{
    public class BudgetRequestAccess : IBudgetRequestAccess
    {
        public void AddBudgetRequest(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.BudgetRequests.Add(request);
                context.SaveChanges();
            }
        }

        public void UpdateBudgetRequest(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                BudgetRequest oldRequest = context.BudgetRequests.FirstOrDefault(e => e.Id == request.Id);
                oldRequest = request;
                context.Entry(oldRequest).State = EntityState.Modified;
                context.SaveChanges();
            }
                
        }

        public void DeleteBudgetRequest(Guid budgetRequestId)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                BudgetRequest request = context.BudgetRequests.FirstOrDefault(e => e.Id == budgetRequestId);
                context.BudgetRequests.Remove(request);
                context.SaveChanges();
            }
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
