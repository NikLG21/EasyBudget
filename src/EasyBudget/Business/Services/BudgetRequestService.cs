using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestService : IBudgetRequestService
    {
        private IBudgetRequestQueries budgetRequestQueries;
        private IBudgetRequestAccess budgetRequestAccess;
        private IUserAccess userAccess;

        public BudgetRequestService(IBudgetRequestQueries budgetRequestQueries, IBudgetRequestAccess budgetRequestAccess)
        {
            this.budgetRequestQueries = budgetRequestQueries;
            this.budgetRequestAccess = budgetRequestAccess;
        }
        public void AddRequest(Guid userId,BudgetRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    throw new LackMandatoryInformation("Назва запиту");
                }
                User user = userAccess.Get(userId);
                request.State = BudgetState.Requested;
                request.DateRequested = DateTime.Today;
                request.Requester = user;
                request.Unit = user.Unit;
                budgetRequestAccess.Add(request);
            }
            catch (DuplicateEntryException)
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
        public void AddRequestByAdmin(Guid userId, Guid id, BudgetRequest request)
        {
            try
            {
                User user = userAccess.Get(id);
                request.State = BudgetState.Requested;
                request.DateRequested = DateTime.Today;
                request.Requester = user;
                request.Unit = user.Unit;
                budgetRequestAccess.Add(request);
            }
            catch (DuplicateEntryException)
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
        public void UpdateByRequestor(Guid userId,BudgetRequest request)
        {
            try
            {
                if (request.State == BudgetState.Requested)
                {
                    budgetRequestAccess.Update(request);
                }
                else
                {
                    throw new NonDeletedUpdatedRequestException("Оновлення");
                }
                
            }
            catch (DuplicateEntryException)
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
        public void DeleteBudgetRequest(Guid userId,BudgetRequest request)
        {
            try
            {
                BudgetRequest request1 = Get(userId, request.Id);
                if (request != request1)
                {
                    throw new EntityUpdatedException("Запит");
                }
                if (request.State == BudgetState.Requested)
                {
                    budgetRequestAccess.Delete(request.Id);
                }
                else
                {
                    throw new NonDeletedUpdatedRequestException("Видалення");
                }
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
        public BudgetRequest Get(Guid userId,Guid requestId)
        {
            try
            {
                return budgetRequestAccess.Get(requestId);
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
        public List<BudgetRequestMainListDto> GetListByRequestor(Guid userId, DateTime start , DateTime finish)
        {
            try
            {
                if (start.Equals(null))
                {
                    start = DateTime.MinValue;
                }

                if (finish.Equals(null))
                {
                    finish = DateTime.MaxValue;
                }
                return budgetRequestQueries.GetBudgetRequestsByRequestor(userId, start, finish);
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
        public List<BudgetRequestMainListDto> GetListByApprover(Guid userId, DateTime start, DateTime finish)
        {
            try
            {
                if (start.Equals(null))
                {
                    start = DateTime.MinValue;
                }

                if (finish.Equals(null))
                {
                    finish = DateTime.MaxValue;
                }
                return budgetRequestQueries.GetBudgetRequestsByApprover(userId, start, finish);
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
        public List<BudgetRequestMainListDto> GetListByExecutor(Guid userId, DateTime start, DateTime finish)
        {
            try
            {
                if (start.Equals(null))
                {
                    start = DateTime.MinValue;
                }

                if (finish.Equals(null))
                {
                    finish = DateTime.MaxValue;
                }
                return budgetRequestQueries.GetBudgetRequestByExecutor(userId, start, finish);
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
        public List<BudgetRequestMainListDto> GetListByTime(DateTime start, DateTime finish)
        {
            try
            {
                if (start.Equals(null))
                {
                    start = DateTime.MinValue;
                }

                if (finish.Equals(null))
                {
                    finish = DateTime.MaxValue;
                }
                return budgetRequestQueries.GetBudgetRequestByTime(start, finish);
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
        public List<BudgetRequestMainListDto> GetListUnapprovedDirector(Guid userId)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.ExecutorEstimated);
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
        public List<BudgetRequestMainListDto> GetListUnapprovedFinDirector(Guid userId)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.ApprovedDirector);
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
        public List<BudgetRequestMainListDto> GetListPostponedFinDirector(Guid userId)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.PostpondFinDirector);
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
        public List<BudgetRequestMainListDto> GetListPostponedDirector(Guid userId)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.PostpondDirector);
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
        public List<BudgetRequestMainListDto> GetListUncheckedExecutor(Guid userId,Department department)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestUncheckedExecutor(department);
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
        public List<BudgetRequestMainListDto> GetListUnapprovedApprover(Guid userId,Unit unit)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestUnapprovedApprover(unit);
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
        public List<BudgetRequestMainListDto> GetListExecutionExecutor(Guid userId, Department department)
        {
            try
            {
                return budgetRequestQueries.GetBudgetRequestExecution(department);
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
