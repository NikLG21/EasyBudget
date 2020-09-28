using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using EasyBudget.Common.Utils;

namespace EasyBudget.Business.Services
{
     public class AgreementBudgetRequestService : IAgreementBudgetRequestService 
     {
         private readonly IBudgetRequestAccess _budgetRequestAccess;

         public AgreementBudgetRequestService(IBudgetRequestAccess budgetRequestAccess)
         {
             _budgetRequestAccess = budgetRequestAccess;
         }

         public void ApproveFirstLine(Guid userId,Guid id)
         {
             try
             {
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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

         public Output ApproveListFirstLine(List<Guid> requestsIds)
         {
             List<BudgetRequestMainListDto> budgetRequests = _budgetRequestAccess.GetList(requestsIds);
             Output output = new Output();
             foreach (BudgetRequestMainListDto request in budgetRequests)
             {
                if (request.State == BudgetState.Requested)
                {
                    request.State = BudgetState.ApprovedFirstLine;
                    output.Success.Add(request);
                }
                else
                {
                    output.Success.Add(request);
                    output.Messages.Add("\""+request.Name+"\": неможливо затвердити. Запит був видалений або змінений");
                }
             }
             _budgetRequestAccess.UpdateList(budgetRequests);
             return output;
         }
         public void ApproveDirector(Guid userId,Guid id)
         {
             try
             {
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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

         public Output ApproveListDirector(List<Guid> requestsIds)
         {
            List<BudgetRequestMainListDto> budgetRequests = _budgetRequestAccess.GetList(requestsIds);
            Output output = new Output();
            foreach (BudgetRequestMainListDto request in budgetRequests)
            {
                if (request.State == BudgetState.ExecutorEstimated| request.State == BudgetState.PostpondDirector)
                {
                    request.State = BudgetState.ApprovedFirstLine;
                    output.Success.Add(request);

                }
                else
                {
                    output.Success.Add(request);
                    output.Messages.Add("\"" + request.Name + "\": неможливо затвердити. Запит був видалений або змінений");
                }
            }
            _budgetRequestAccess.UpdateList(budgetRequests);
            return output;
        }

         public Output ExecutionStartedListFinDirector(List<Guid> requestsIds)
         {
             throw new NotImplementedException();
         }

         public void RejectFirstLine(Guid userId, Guid id)
         {
             try
             {
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
                 BudgetRequest request = _budgetRequestAccess.Get(id);
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
