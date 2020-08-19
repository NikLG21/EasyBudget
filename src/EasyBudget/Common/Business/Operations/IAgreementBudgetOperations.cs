﻿using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Operations
{
    interface IAgreementBudgetOperations
    {
        void ApproveBudgetRequest(Guid id);
        void RejectBudgetRequest(Guid id);
        void SpecifyBudgetRequest(Guid id, User user);
        void DelayBudgetRequest(Guid id, DateTime delayTime);
    }
}