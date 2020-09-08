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
        private readonly IBudgetRequestQueries _budgetRequestQueries;
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IUserAccess _userAccess;

        public BudgetRequestService(
            IBudgetRequestQueries budgetRequestQueries,
            IBudgetRequestAccess budgetRequestAccess, 
            IUserAccess userAccess)
        {
            _budgetRequestQueries = budgetRequestQueries;
            _budgetRequestAccess = budgetRequestAccess;
            _userAccess = userAccess;
        }

        public void AddRequest(Guid userId,BudgetRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    throw new LackMandatoryInformation("Назва запиту");
                }
                User user = _userAccess.Get(userId);
                request.State = BudgetState.Requested;
                request.DateRequested = DateTime.Today;
                request.Requester = user;
                request.Unit = user.Unit;
                _budgetRequestAccess.Add(request);
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
                User user = _userAccess.Get(id);
                request.State = BudgetState.Requested;
                request.DateRequested = DateTime.Today;
                request.Requester = user;
                request.Unit = user.Unit;
                _budgetRequestAccess.Add(request);
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
                    _budgetRequestAccess.Update(request);
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
                    _budgetRequestAccess.Delete(request.Id);
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
                return _budgetRequestAccess.Get(requestId);
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
        public List<BudgetRequestMainListDto> GetListByRequestor(Guid userId, DateTime start, DateTime finish)
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
                return _budgetRequestQueries.GetBudgetRequestsByRequestor(userId, start, finish);
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
                return _budgetRequestQueries.GetBudgetRequestsByApprover(userId, start, finish);
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
                return _budgetRequestQueries.GetBudgetRequestByExecutor(userId, start, finish);
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
        public List<BudgetRequestMainListDto> GetListByTime(Guid userId, DateTime start, DateTime finish)
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
                return _budgetRequestQueries.GetBudgetRequestByTime(start, finish);
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
                return _budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.ExecutorEstimated);
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
                return _budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.ApprovedDirector);
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
                return _budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.PostpondFinDirector);
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
                return _budgetRequestQueries.GetBudgetRequestUnapprovedDirectors(BudgetState.PostpondDirector);
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
        public List<BudgetRequestMainListDto> GetListUncheckedExecutor(Guid userId, Department department)
        {
            try
            {
                return _budgetRequestQueries.GetBudgetRequestUncheckedExecutor(department);
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
        public List<BudgetRequestMainListDto> GetListUnapprovedApprover(Guid userId, Unit unit)
        {
            try
            {
                return _budgetRequestQueries.GetBudgetRequestUnapprovedApprover(unit);
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
                return _budgetRequestQueries.GetBudgetRequestExecution(department);
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

        public List<BudgetRequestMainListDto> GetListUnapprovedRequestor(Guid userId)
        {
            try
            {
                return _budgetRequestQueries.GetBudgetRequestUnapprovedRequestor(userId);
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
