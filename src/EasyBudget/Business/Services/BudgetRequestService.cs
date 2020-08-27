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

        public BudgetRequestService(IBudgetRequestQueries budgetRequestQueries, IBudgetRequestAccess budgetRequestAccess)
        {
            this.budgetRequestQueries = budgetRequestQueries;
            this.budgetRequestAccess = budgetRequestAccess;
        }

        public void Add(BudgetRequest request)
        {
            try
            {
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
        public void UpdateByRequestor(BudgetRequest request)
        {
            try
            {
                if (request.State == BudgetState.Requested)
                {
                    budgetRequestAccess.Update(request);
                }
                else
                {
                    throw new NonDeletedUpdatedRequestException("Обновление");
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
        public void DeleteBudgetRequest(BudgetRequest request)
        {
            try
            {
                BudgetRequest request1 = Get(request.Id);
                if (request != request1)
                {
                    throw new EntityUpdatedException("Запрос");
                }
                if (request.State == BudgetState.Requested)
                {
                    budgetRequestAccess.Delete(request.Id);
                }
                else
                {
                    throw new NonDeletedUpdatedRequestException("Удаление");
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
        public BudgetRequest Get(Guid id)
        {
            try
            {
                return budgetRequestAccess.Get(id);
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
        public List<BudgetRequestMainListDto> GetListUnapprovedDirector()
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
        public List<BudgetRequestMainListDto> GetListUnapprovedFinDirector()
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
        public List<BudgetRequestMainListDto> GetListPostponedFinDirector()
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
        public List<BudgetRequestMainListDto> GetListPostponedDirector()
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
        public List<BudgetRequestMainListDto> GetListUncheckedExecutor(Department department)
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
        public List<BudgetRequestMainListDto> GetListUnapprovedApprover(Unit unit)
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

    }
}
