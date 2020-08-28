using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Services
{
    public interface IBudgetDescriptionService
    {
        void AddBudgetDescription(Guid userId, BudgetDescription description);
        BudgetDescription GetBudgetDescription(Guid userId, Guid descriptionId);
        List<BudgetDescriptionMainListDto> GetBudgetDescriptionListByRequest(Guid userId, Guid budgetRequestId);

    }
}
