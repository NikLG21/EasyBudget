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

        public void AddBudgetRequest(BudgetRequest request)
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

        public void UpdateBudgetRequest(BudgetRequest request)
        {
            try
            {
                budgetRequestAccess.Update(request);
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
                BudgetRequest request1 = GetBudgetRequest(request.Id);
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
                    throw new NonDeletedBudgetRequestException();
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

        public BudgetRequest GetBudgetRequest(Guid id)
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

        public List<BudgetRequestMainListDto> ViewBudgetRequestsList(User user, DateTime start , DateTime finish, List<Role> roles)
        {
            try
            {
                List<BudgetRequestMainListDto> list = new List<BudgetRequestMainListDto>();
                //for (int i = 0; i < roles.Count; i++)
                //{
                //    switch (roles[i].Name)
                //    {
                //        case "Инициатор запроса":
                //            list.AddRange(budgetRequestQueries.GetBudgetRequestsByRequestor(user.Id));
                //            break;
                //        case "Утверждающий":
                //            list.AddRange(budgetRequestQueries.GetBudgetRequestsByApprover(user.Id));
                //            break;
                //        case "Исполнитель IT":
                //            list.AddRange(budgetRequestQueries.GetBudgetRequestByExecutor(user.Id));
                //            break;
                //        case "Исполнитель Хозчасть":
                //            list.AddRange(budgetRequestQueries.GetBudgetRequestByExecutor(user.Id));
                //            break;
                //    }
                //}
                return list;

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
