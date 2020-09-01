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
         private readonly IBudgetRequestService _budgetRequestService;
         private readonly IBudgetRequestAccess _budgetRequestAccess;

         public AgreementBudgetRequestService(
             IBudgetRequestService budgetRequestService, 
             IBudgetRequestAccess budgetRequestAccess)
         {
             _budgetRequestService = budgetRequestService;
             _budgetRequestAccess = budgetRequestAccess;
         }

         public void ApproveFirstLine(Guid userId,Guid id)
         {
             try
             {
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.Requested)
                 {
                     request.State = BudgetState.ApprovedFirstLine;
                     _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.ExecutorEstimated|request.State == BudgetState.PostpondDirector)
                 {
                     request.State = BudgetState.ApprovedDirector;
                     request.DateDirectorApprove = DateTime.Today;
                     _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.Requested)
                 {
                     request.State = BudgetState.RejectedFirstLine;
                     _budgetRequestAccess.Update(request);
                 }
                 request.State = BudgetState.RejectedFirstLine;
                 _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.ExecutorEstimated|request.State == BudgetState.PostpondDirector)
                 {
                     request.State = BudgetState.RejectedDirector;
                     _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.ExecutorEstimated)
                 {
                     request.State = BudgetState.PostpondDirector;
                     _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.ApprovedDirector)
                 {
                     request.State = BudgetState.PostpondFinDirector;
                     _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.ApprovedDirector| request.State == BudgetState.PostpondFinDirector)
                 {
                     request.State = BudgetState.Execution;
                     request.DateDeadlineExecution = deadline; 
                     request.DateStartExecution = DateTime.Today;
                     _budgetRequestAccess.Update(request);
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
                 BudgetRequest request = _budgetRequestService.Get(userId, id);
                 if (request.State == BudgetState.ApprovedFirstLine)
                 {
                     request.State = BudgetState.PostpondFinDirector;
                     request.RealPrice = realPrice;
                     _budgetRequestAccess.Update(request);
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
         public void ExecutionFinishedFinDirector(Guid userId, Guid id)
         {
             try
             {
                 BudgetRequest request = _budgetRequestService.Get(userId,id);
                 if (request.State == BudgetState.Execution)
                 {
                     request.State = BudgetState.Executed;
                     request.DateEndExecution = DateTime.Today;
                     _budgetRequestAccess.Update(request);
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
