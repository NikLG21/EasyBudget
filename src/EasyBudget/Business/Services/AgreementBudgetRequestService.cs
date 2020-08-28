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
         public void ApproveFirstLine(Guid userId,Guid id)
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
         public void ApproveDirector(Guid userId,Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ExecutorEstimated|request.State == BudgetState.PostpondDirector)
                 {
                     request.State = BudgetState.ApprovedDirector;
                     request.DateDirectorApprove = DateTime.Today;
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
         public void RejectFirstLine(Guid userId, Guid id)
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
         public void RejectDirector(Guid userId, Guid id)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ExecutorEstimated|request.State == BudgetState.PostpondDirector)
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
         public void PostponedDirector(Guid userId, Guid id)
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
         public void PostponedFinDirector(Guid userId, Guid id)
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
         public void ExecutionStartedFinDirector(Guid userId, Guid id, DateTime deadline)
         {
             try
             {
                 BudgetRequest request = budgetRequestService.Get(id);
                 if (request.State == BudgetState.ApprovedDirector| request.State == BudgetState.PostpondFinDirector)
                 {
                     request.State = BudgetState.Execution;
                     request.DateDeadlineExecution = deadline; 
                     request.DateStartExecution = DateTime.Today;
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
         public void RealPriceAdded(Guid userId, Guid id, decimal realPrice)
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
     }
}
