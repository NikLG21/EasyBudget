using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services.AgreementBudgetRequestServices
{
    public class AgreementFirstLineService : IAgreementFirstLineService
    {
        private readonly IBudgetRequestAccess _budgetRequestAccess;
        private readonly IBudgetRequestListQueries _budgetRequestListQueries;
        public AgreementFirstLineService(IBudgetRequestAccess budgetRequestAccess, IBudgetRequestListQueries budgetRequestListQueries)
        {
            _budgetRequestAccess = budgetRequestAccess;
            _budgetRequestListQueries = budgetRequestListQueries;
        }

        public void ApproveFirstLine(Guid userId, Guid id)
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

        //public BudgetRequestUpdateOutput ApproveListFirstLine(List<Guid> requestsIds)
        //{
        //    List<BudgetRequestMainListDto> budgetRequests = _budgetRequestListQueries.GetListByIds(requestsIds);
        //    BudgetRequestUpdateOutput output = new BudgetRequestUpdateOutput();
        //    List<Guid> ids = new List<Guid>();
        //    foreach (BudgetRequestMainListDto request in budgetRequests)
        //    {
        //        if (request.State == BudgetState.Requested)
        //        {
        //            request.State = BudgetState.ApprovedFirstLine;
        //            output.SuccessUpdatedBudgetRequests.Add(request);
        //            ids.Add(request.Id);
        //        }
        //        else
        //        {
        //            output.SuccessUpdatedBudgetRequests.Add(request);
        //            output.Messages.Add("\"" + request.Name + "\": неможливо затвердити. Запит був видалений або змінений");
        //        }
        //    }
        //    _budgetRequestAccess.UpdateList(ids, BudgetState.ApprovedFirstLine);
        //    return output;
        //}
    }
}
