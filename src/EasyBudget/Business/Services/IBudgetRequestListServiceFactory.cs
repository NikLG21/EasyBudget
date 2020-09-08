using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
    public interface IBudgetRequestListServiceFactory
    {
        IBudgetRequestListService Create(Role role);
    }
}
