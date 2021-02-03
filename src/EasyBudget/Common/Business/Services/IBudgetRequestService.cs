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
        //TODO: Non correspondent names. Probably better UpdateRequestByRequester
        BudgetRequestUpdateOutput UpdateByRequester(UserMainInfoDto userInfo, BudgetRequest request);
        //TODO: Non correspondent names. Probably better DeleteRequest
        BudgetRequestUpdateOutput DeleteBudgetRequest(UserMainInfoDto userInfo,BudgetRequest request);
        //TODO: Non correspondent names. Probably better GetRequestBy...
        BudgetRequest Get(Guid userId,Guid requestId);
    }
}
