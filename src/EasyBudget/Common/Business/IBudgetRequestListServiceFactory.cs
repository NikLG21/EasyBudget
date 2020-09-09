using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business
{
    public interface IBudgetRequestListServiceFactory
    {
        IBudgetRequestListService Create(Role role);
    }
}
