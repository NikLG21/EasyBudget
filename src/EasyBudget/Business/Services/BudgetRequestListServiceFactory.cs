using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestListServiceFactory : IBudgetRequestListServiceFactory
    {
        private IBudgetRequestListQueries _budgetRequestListQueries;

        public BudgetRequestListServiceFactory(IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestListQueries = budgetRequestListQueries;
        }
        public IBudgetRequestListService Create(Role role)
        {
            IBudgetRequestListService budgetRequestListService;
            switch (role.Name)
            {
                case "Ініціатор запиту":
                    budgetRequestListService= new BudgetRequestListRequestorService(_budgetRequestListQueries);
                    break;
                case "Затверджувач":
                    budgetRequestListService = new BudgetRequestListApproverService(_budgetRequestListQueries);
                    break;
                case "Виконавець":
                    budgetRequestListService = new BudgetRequestListExecutorService(_budgetRequestListQueries);
                    break;
                case "Виконавець IT":
                    budgetRequestListService = new BudgetRequestListExecutorService(_budgetRequestListQueries);
                    break;
                case "Директор":
                    budgetRequestListService = new BudgetRequestListDirectorService(_budgetRequestListQueries);
                    break;
                case "Фінансовий директор":
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
