using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Commands
{
    public interface IBudgetRequestCommands
    {
        void UpdateList(List<Guid> ids, BudgetState newState, Guid userId);
    }
}
