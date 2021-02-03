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
        //TODO: Non correspondent names. Probably better UpdateRequestByRequester. Done
        BudgetRequestUpdateOutput UpdateRequestByRequester(UserMainInfoDto userInfo, BudgetRequest request);
        //TODO: Non correspondent names. Probably better DeleteRequest. Done
        BudgetRequestUpdateOutput DeleteRequest(UserMainInfoDto userInfo,BudgetRequest request);
        //TODO: Non correspondent names. Probably better GetRequestBy...
        BudgetRequest GetRequest(Guid userId,Guid requestId);
    }
}
