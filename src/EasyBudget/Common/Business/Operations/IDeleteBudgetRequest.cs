using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Operations
{
    interface IDeleteBudgetRequest
    {
        void DeleteBudgetRequest(Guid id);
    }
}
