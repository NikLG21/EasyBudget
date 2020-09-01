using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Ui
{
    public static class ContainerConfigurator
    {
        public static void ConfigureContainer(this IUnityContainer container)
        {
            //container.RegisterType<IEventAggregator, EventAggregator>(new HierarchicalLifetimeManager());
        }
    }
}
