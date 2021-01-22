using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestService
    {
        BudgetRequestUpdateOutput AddRequest(Guid userId, BudgetRequest request);
        void AddRequestByAdmin(Guid userId, Guid requestorUserId, BudgetRequest request);
        BudgetRequestUpdateOutput UpdateByRequester(UserMainInfoDto userInfo, BudgetRequest request);
        BudgetRequestUpdateOutput DeleteBudgetRequest(UserMainInfoDto userInfo,BudgetRequest request);
        BudgetRequest Get(Guid userId,Guid requestId);
    }
}
