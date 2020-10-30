using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;

namespace EasyBudget.Business.Factories
{
    public class AgreementServiceFactory : IAgreementServiceFactory
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;

        public AgreementServiceFactory(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }

        public IAgreementBaseService GetBase()
        {
            return new AgreementBaseService(_budgetRequestAccess, _budgetRequestListQueries);
        }

        public IAgreementDirectorService GetDirector()
        {
            return new AgreementDirectorService(_budgetRequestAccess, _budgetRequestListQueries);
        }

        public IAgreementExecutorService GetExecutor()
        {
            return new AgreementExecutorService(_budgetRequestAccess);
        }

        public IAgreementFinDirectorService GetFinDirector()
        {
            return new AgreementFinDirectorService(_budgetRequestAccess, _budgetRequestListQueries);
        }

        public IAgreementFirstLineService GetFirstLine()
        {
            return  new AgreementFirstLineService(_budgetRequestAccess, _budgetRequestListQueries);
        }
    }
}
