using EasyBudget.Business.Services;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Factories
{
    public class BudgetRequestListServiceFactory : IBudgetRequestListServiceFactory
    {
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;

        public BudgetRequestListServiceFactory(IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestListQueries = budgetRequestListQueries;
        }
        public IBudgetRequestListService Create(Role role)
        {
            IBudgetRequestListService budgetRequestListService;
            switch (role.Name)
            {
                case RoleNames.Requester:
                    budgetRequestListService= new BudgetRequestListRequesterService(_budgetRequestListQueries);
                    break;
                case RoleNames.Approver:
                    budgetRequestListService = new BudgetRequestListApproverService(_budgetRequestListQueries);
                    break;
                case RoleNames.Executor:
                    budgetRequestListService = new BudgetRequestListExecutorService(_budgetRequestListQueries);
                    break;
                case RoleNames.Director:
                case RoleNames.FinDirector:
                    budgetRequestListService = new BudgetRequestListDirectorService(_budgetRequestListQueries);
                    break;
                default:
                    budgetRequestListService = null;
                    break;
            }

            return budgetRequestListService;
        }
    }
}
