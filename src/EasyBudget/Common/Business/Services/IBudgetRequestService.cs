using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Services
{
    //TODO: Very big service. Please, split it into several. E.g per role
    //TODO: Why need IBudgetRequestListService?
    public interface IBudgetRequestService
    {
        void AddRequest(Guid userId, BudgetRequest request);
        void AddRequestByAdmin(Guid userId, Guid id, BudgetRequest request);
        void UpdateByRequestor(Guid userId,BudgetRequest request);
        void DeleteBudgetRequest(Guid userId,BudgetRequest request);
        BudgetRequest Get(Guid userId,Guid requestId);

        List<BudgetRequestMainListDto> GetListByRequestor(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListByApprover(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListByExecutor(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListByTime(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListUnapprovedDirector(Guid userId);
        List<BudgetRequestMainListDto> GetListUnapprovedFinDirector(Guid userId);
        List<BudgetRequestMainListDto> GetListPostponedFinDirector(Guid userId);
        List<BudgetRequestMainListDto> GetListPostponedDirector(Guid userId);
        List<BudgetRequestMainListDto> GetListUnapprovedApprover(Guid userId, Unit unit);
        List<BudgetRequestMainListDto> GetListUncheckedExecutor(Guid userId, Department department);
        List<BudgetRequestMainListDto> GetListExecutionExecutor(Guid userId, Department department);

        List<BudgetRequestMainListDto> GetListUnapprovedRequestor(Guid userId);
    }
}
