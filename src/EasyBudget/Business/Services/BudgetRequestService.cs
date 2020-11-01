using System;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
    public class BudgetRequestService : IBudgetRequestService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IUserAccess _userAccess;

        public BudgetRequestService(IBudgetRequestAccess budgetRequestAccess, IUserAccess userAccess)
        {
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

                if (request.Department == null)
                {
                    throw new LackMandatoryInformation("Відділ");
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
        public void AddRequestByAdmin(Guid userId, Guid requestorUserId, BudgetRequest request)
        {
            try
            {
                User user = _userAccess.Get(requestorUserId);
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
        public void UpdateByRequester(Guid userId, BudgetRequest request)
        {
            try
            {
                if (request.State == BudgetState.Requested)
                {
                    _budgetRequestAccess.Update(request);
                }
                else
                {
                    throw new NonDeletedUpdatedRequestException();
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
                    throw new NonDeletedUpdatedRequestException();
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
    }
}
