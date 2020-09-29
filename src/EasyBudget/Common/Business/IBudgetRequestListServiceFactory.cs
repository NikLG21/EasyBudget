using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business
{
    //TODO: Please move into Factories folder
    public interface IBudgetRequestListServiceFactory
    {
        IBudgetRequestListService Create(Role role);
    }
}
