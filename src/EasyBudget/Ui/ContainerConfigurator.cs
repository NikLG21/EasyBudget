using DataAccess;
using DataAccess.Access;
using DataAccess.Queries;
using EasyBudget.Business.Factories;
using EasyBudget.Business.Services;
using EasyBudget.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Business.Services.UserServices;
using EasyBudget.Common.Business;
using EasyBudget.Common.Business.Factories;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.Business.Services.AgreementBudgetRequestServices;
using EasyBudget.Common.Business.Services.UserServices;
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
            //business
            container.RegisterType<IBudgetRequestService, BudgetRequestService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAdminUserService, AdminUserService>();
            container.RegisterType<IBudgetDescriptionService, BudgetDescriptionService>();
            container.RegisterType<IAgreementDirectorService, AgreementDirectorService>();
            container.RegisterType<IAgreementFinDirectorService, AgreementFinDirectorService>();
            container.RegisterType<IAgreementFirstLineService, AgreementFirstLineService>();
            container.RegisterType<IAgreementExecutorService, AgreementExecutorService>();
            container.RegisterType<IAgreementBaseService, AgreementBaseService>();
            container.RegisterType<IBudgetRequestListServiceFactory, BudgetRequestListServiceFactory>();
            //dataAccess
            container.RegisterType<IBudgetRequestDbContextFactory, BudgetRequestDbContextFactory>();
            container.RegisterType<IUserAccess, UserAccess>();
            container.RegisterType<IUserQueries, UserQueries>();
            container.RegisterType<IBudgetDescriptionAccess, BudgetDescriptionAccess>();
            container.RegisterType<IBudgetDescriptionQueries, BudgetDescriptionQueries>();
            container.RegisterType<IBudgetRequestAccess,BudgetRequestAccess>();
            container.RegisterType<IDepartmentAccess, DepartmentAccess>();
            container.RegisterType<IBudgetRequestListQueries, BudgetRequestListQueries>();
            //Presentation
            container.RegisterType<IBudgetRequestViewModel,BudgetRequestViewModel>();
            container.RegisterType<IBudgetRequestListViewModel, BudgetRequestListViewModel>();
            
        }
    }
}
