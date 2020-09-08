using DataAccess;
using DataAccess.Access;
using DataAccess.Queries;
using EasyBudget.Business.Services;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Presentation.Interfaces;
using EasyBudget.Presentation.ViewModels;
using Unity;

namespace EasyBudget.Ui
{
    public static class ContainerConfigurator
    {
        public static void ConfigureContainer(this IUnityContainer container)
        {
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IBudgetRequestService, BudgetRequestService>();
            container.RegisterType<IBudgetDescriptionService, BudgetDescriptionService>();
            container.RegisterType<IAgreementBudgetRequestService, AgreementBudgetRequestService>();
            container.RegisterType<IBudgetRequestListService, BudgetRequestListApproverService>("Approver");
            container.RegisterType<IBudgetRequestListService, BudgetRequestListDirectorService>("Director");
            container.RegisterType<IBudgetRequestListService, BudgetRequestListExecutorService>("Executor");
            container.RegisterType<IBudgetRequestListService, BudgetRequestListRequestorService>("Requestor");
            container.RegisterType<IBudgetRequestListServiceFactory, BudgetRequestListServiceFactory>();

            container.RegisterType<IUserAccess, UserAccess>();
            container.RegisterType<IUserQueries, UserQueries>();
            container.RegisterType<IBudgetDescriptionAccess, BudgetDescriptionAccess>();
            container.RegisterType<IBudgetDescriptionQueries, BudgetDescriptionQueries>();
            container.RegisterType<IBudgetRequestAccess,BudgetRequestAccess>();
            container.RegisterType<IBudgetRequestQueries, BudgetRequestQueries>();
            container.RegisterType<IDepartmentAccess, DepartmentAccess>();
            container.RegisterType<IBudgetRequestListQueries, BudgetRequestListQueries>();

            container.RegisterType<IBudgetRequestViewModel,BudgetRequestViewModel>();

            container.RegisterType<IBudgetRequestRowViewModel, BudgetRequestRowViewModel>();

            container.RegisterType<IBudgetRequestDbContextFactory, BudgetRequestDbContextFactory>();

        }
    }
}
