using System;
using System.Data.Entity;
using System.Linq;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Model;

namespace DataAccess.Access
{
    public class BudgetRequestAccess : IBudgetRequestAccess
    {
        public void Add(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.BudgetRequests.Add(request);
                context.Entry(request).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(BudgetRequest request)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                context.BudgetRequests.Add(request);
                context.Entry(request).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Guid Id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                BudgetRequest request = context.BudgetRequests.FirstOrDefault(e => e.Id == Id);
                if (request != null)
                {
                    context.BudgetRequests.Attach(request);
                    context.BudgetRequests.Remove(request);
                    context.SaveChanges();
                }
            }
        }

        public BudgetRequest Get(Guid Id)
        {
            using (BudgetRequestDbContext context = new BudgetRequestDbContext())
            {
                return context.BudgetRequests.AsNoTracking().FirstOrDefault(e => e.Id == Id);
            }
        }

        //public List<BudgetRequest> GetBudgetRequestByOriginatorList(Guid userId)
        //{
        //    using (BudgetRequestDbContext context = new BudgetRequestDbContext())
        //    {
        //        List<BudgetRequest> budgetRequests = new List<BudgetRequest>();
                
        //        foreach (BudgetRequest budgetRequest in context.BudgetRequests)
        //        {
        //            if (budgetRequest.Requester.Id.Equals(userId))
        //            {
        //                budgetRequests.Add(budgetRequest);
        //            }
        //        }

        //        return budgetRequests;
        //    }
        //}

        //public List<BudgetRequest> GetBudgetRequestByApproverList(Guid userId)
        //{
        //    using (BudgetRequestDbContext context = new BudgetRequestDbContext())
        //    {
        //        List<BudgetRequest> budgetRequests = new List<BudgetRequest>();

        //        foreach (BudgetRequest budgetRequest in context.BudgetRequests)
        //        {
        //            if (budgetRequest.Approver.Id.Equals(userId))
        //            {
        //                budgetRequests.Add(budgetRequest);
        //            }
        //        }

        //        return budgetRequests;
        //    }
        //}
    }
}
