using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Services;
using EasyBudget.Presentation.Interfaces;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Factory
{
    public class BudgetRequestEntityFactory : IBudgetRequestEntityFactory
    {
        private readonly IBudgetRequestService _budgetRequestService;
        private readonly IAgreementServiceFactory _agreementServiceFactory;
        private readonly IDepartmentService _departmentService;

        public BudgetRequestEntityFactory(IBudgetRequestService budgetRequestService, IAgreementServiceFactory agreementServiceFactory, IDepartmentService departmentService)
        {
            _budgetRequestService = budgetRequestService;
            _agreementServiceFactory = agreementServiceFactory;
            _departmentService = departmentService;
        }

        public IBudgetRequestViewModel GetNewRequestViewModel()
        {
            return new BudgetRequestViewModel(_budgetRequestService,_agreementServiceFactory,_departmentService);
        }

        public IBudgetRequestViewModel GetExistedRequestViewModel(Guid id)
        {
            return new BudgetRequestViewModel(id, _budgetRequestService, _agreementServiceFactory);
        }
    }
}
