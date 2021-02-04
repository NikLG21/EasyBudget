using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;

namespace EasyBudget.Common.Business.Factories
{
    public interface IAgreementServiceFactory
    {
        //TODO: Why need this. Easier access to agreement services by role. Doesn't need view model per role.
        IAgreementCommonService GetCommon();
        IAgreementDirectorService GetDirector();
        IAgreementExecutorService GetExecutor();
        IAgreementFinDirectorService GetFinDirector();
        IAgreementFirstLineService GetFirstLine();
    }
}
