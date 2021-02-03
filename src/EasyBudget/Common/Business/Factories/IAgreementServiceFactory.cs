﻿using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;

namespace EasyBudget.Common.Business.Factories
{
    public interface IAgreementServiceFactory
    {
        //TODO: Why need this
        IAgreementBaseService GetBase();
        IAgreementDirectorService GetDirector();
        IAgreementExecutorService GetExecutor();
        IAgreementFinDirectorService GetFinDirector();
        IAgreementFirstLineService GetFirstLine();
    }
}
