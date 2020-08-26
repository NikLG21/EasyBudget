using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestService
    {
        void Add(BudgetRequest request);
        void UpdateByRequestor(BudgetRequest request);
        void DeleteBudgetRequest(BudgetRequest request);
        BudgetRequest Get(Guid id);
        List<BudgetRequestMainListDto> GetListByRequestor(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListByApprover(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListByExecutor(Guid userId, DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListByTime(DateTime start, DateTime finish);
        List<BudgetRequestMainListDto> GetListUnapprovedDirector();
        List<BudgetRequestMainListDto> GetListUnapprovedFinDirector();
        List<BudgetRequestMainListDto> GetListPostponedFinDirector();
        List<BudgetRequestMainListDto> GetListPostponedDirector();
    }
}
