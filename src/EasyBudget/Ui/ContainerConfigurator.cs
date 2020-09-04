using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using DataAccess.Access;
using DataAccess.Queries;
using EasyBudget.Business.Services;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;
using Microsoft.Extensions.Configuration;
using Unity;

namespace Ui
{
    public static class ContainerConfigurator
    {
        public static void ConfigureContainer(this IUnityContainer container)
        {
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IBudgetRequestService, BudgetRequestService>();
            container.RegisterType<IBudgetDescriptionService, BudgetDescriptionService>();
            container.RegisterType<IAgreementBudgetRequestService, AgreementBudgetRequestService>();

            container.RegisterType<IUserAccess, UserAccess>();
            container.RegisterType<IUserQueries, UserQueries>();
            container.RegisterType<IBudgetDescriptionAccess, BudgetDescriptionAccess>();
            container.RegisterType<IBudgetDescriptionQueries, BudgetDescriptionQueries>();
            container.RegisterType<IBudgetRequestAccess,BudgetRequestAccess>();
            container.RegisterType<IBudgetRequestQueries, BudgetRequestQueries>();
            container.RegisterType<IDepartmentAccess, DepartmentAccess>();

            container.RegisterType<IBudgetRequestDbContextFactory, BudgetRequestDbContextFactory>();
        }
    }
}
