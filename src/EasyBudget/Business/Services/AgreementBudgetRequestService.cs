using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
     public class AgreementBudgetRequestService : IAgreementBudgetRequestService 
     {
         private IBudgetRequestService budgetRequestService;
         private IBudgetRequestAccess budgetRequestAccess; 
         public void ApproveFirstLine(Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.Requested)
                 {
                     request.State = BudgetState.ApprovedFirstLine;
                     budgetRequestAccess.Update(request);
                 }
             }
             catch (EntityNotFoundException)
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
         public void ApproveDirector(Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ExecutorEstimated)
                 {
                     request.State = BudgetState.ApprovedDirector;
                     budgetRequestAccess.Update(request);
                 }

             }
             catch (EntityNotFoundException)
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
         public void RejectFirstLine(Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.Requested)
                 {
                     request.State = BudgetState.RejectedFirstLine;
                     budgetRequestAccess.Update(request);
                 }
                 request.State = BudgetState.RejectedFirstLine;
                 budgetRequestAccess.Update(request);
             }
             catch (EntityNotFoundException)
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
         public void RejectDirector(Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ExecutorEstimated)
                 {
                     request.State = BudgetState.RejectedDirector;
                     budgetRequestAccess.Update(request);
                 }
             }
             catch (EntityNotFoundException)
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
         public void PostponedDirector(Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ExecutorEstimated)
                 {
                     request.State = BudgetState.PostpondDirector;
                     budgetRequestAccess.Update(request);
                 }

             }
             catch (EntityNotFoundException)
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
         public void PostponedFinDirector(Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ApprovedDirector)
                 {
                     request.State = BudgetState.PostpondFinDirector;
                     budgetRequestAccess.Update(request);
                 }
             }
             catch (EntityNotFoundException)
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
         public void RealPriceAdded(Guid id, decimal realPrice)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ApprovedFirstLine)
                 {
                     request.State = BudgetState.PostpondFinDirector;
                     request.RealPrice = realPrice;
                     budgetRequestAccess.Update(request);
                 }
             }
             catch (EntityNotFoundException)
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
         public void SpecifyBudgetRequest(Guid id, User user) 
         {
            throw new NotImplementedException();
         }
     }
}
