using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetRequestService
    {
        void AddBudgetRequest(BudgetRequest request);
        void UpdateBudgetRequest(BudgetRequest request);
        void DeleteBudgetRequest(BudgetRequest request);

        BudgetRequest GetBudgetRequest(Guid id);

        List<BudgetRequestMainListDto> ViewBudgetRequestsList(User user, DateTime start, DateTime finish,  List<Role> roles);
    }
}
