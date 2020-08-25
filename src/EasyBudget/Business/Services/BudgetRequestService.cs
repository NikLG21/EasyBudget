using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestService : IBudgetRequestService
    {
        private IBudgetRequestQueries budgetRequestQueries;
        private IBudgetRequestAccess budgetRequestAccess;

        public BudgetRequestService(IBudgetRequestQueries budgetRequestQueries, IBudgetRequestAccess budgetRequestAccess)
        {
            this.budgetRequestQueries = budgetRequestQueries;
            this.budgetRequestAccess = budgetRequestAccess;
        }

        public void AddBudgetRequest(BudgetRequest request, User user)
        {
            try
            {

            }
            catch (DuplicateEntryException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public void UpdateBudgetRequest(BudgetRequest request, User user)
        {
            try
            {

            }
            catch (DuplicateEntryException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public void DeleteBudgetRequest(BudgetRequest request, User user)
        {
            try
            {

            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public BudgetRequest ViewBudgetRequest(Guid id, User user)
        {
            try
            {
                return budgetRequestAccess.Get(id);
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public List<BudgetRequest> ViewBudgetRequestsList(User user, DateTime start, DateTime finish)
        {
            try
            {

            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }
    }
}
