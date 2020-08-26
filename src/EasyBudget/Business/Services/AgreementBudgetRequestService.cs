using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
     public class AgreementBudgetRequestService : IAgreementBudgetRequestService
     {
         private IBudgetRequestService budgetRequestService;

         public void ApproveBudgetRequest(Guid id)
         {

         }

         public void RejectBudgetRequest(Guid id)
         {
            throw new NotImplementedException();
         }

         public void SpecifyBudgetRequest(Guid id, User user) 
         {
            throw new NotImplementedException();
         }

         public void DelayBudgetRequest(Guid id, DateTime delayTime)
         {
            throw new NotImplementedException();
         }
     }
}
