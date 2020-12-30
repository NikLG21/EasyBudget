using System;
using EasyBudget.Common.Business.Outputs;
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
        private readonly IDepartmentAccess _departmentAccess;

        public BudgetRequestService(IBudgetRequestAccess budgetRequestAccess, IUserAccess userAccess,IDepartmentAccess departmentAccess)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _userAccess = userAccess;
            _departmentAccess = departmentAccess;
        }

        public BudgetRequestUpdateOutput AddRequest(Guid userId,BudgetRequest request)
        {
            try
            {
                if (request.Name == null)
                {
                    throw new LackMandatoryInformation("Назва запиту");
                }

                if (request.Department.Id.Equals(Guid.Empty))
                {
                    throw new LackMandatoryInformation("Відділ");
                }
                User user = _userAccess.Get(userId);
                Department department = _departmentAccess.Get(request.Department.Id);
                request.Id=Guid.NewGuid();
                request.State = BudgetState.Requested;
                request.DateRequested = DateTime.Today;
                request.Requester = user;
                request.RequesterId = user.Id;
                request.Unit = user.Unit;
                request.UnitId = user.Unit.Id;
                request.Department = department;
                request.DepartmentId = department.Id;
                _budgetRequestAccess.Add(request);
                return new BudgetRequestUpdateOutput(request,"Запит успішно додано");
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
                Department department = _departmentAccess.Get(request.Department.Id);
                request.Id = new Guid();
                request.State = BudgetState.Requested;
                request.DateRequested = DateTime.Today;
                request.Requester = user;
                request.Unit = user.Unit;
                request.Department = department;
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
