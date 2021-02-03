using System;
using EasyBudget.Common.Business.Outputs;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestService
    {
        BudgetRequestUpdateOutput AddRequest(Guid userId, BudgetRequest request);
        void AddRequestByAdmin(Guid userId, Guid requestorUserId, BudgetRequest request);
        BudgetRequestUpdateOutput UpdateRequestByRequester(UserMainInfoDto userInfo, BudgetRequest request);
        BudgetRequestUpdateOutput DeleteRequest(UserMainInfoDto userInfo,BudgetRequest request);
        BudgetRequest GetRequest(Guid userId,Guid requestId);
    }
}
