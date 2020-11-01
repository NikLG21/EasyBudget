using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
using Action = System.Action;

namespace DataAccess.Utils
{
    public static class DataAccessUtils
    {
        public static string Dump(BudgetRequestDbContext context)
        {
            StringBuilder builder = new StringBuilder();
            DumpEntitySet(context, nameof(Action), builder, context.Actions.Local);
            DumpEntitySet(context, nameof(BudgetRequest), builder, context.BudgetRequests.Local);
            DumpEntitySet(context, nameof(BudgetDescription), builder, context.BudgetDescriptions.Local);
            DumpEntitySet(context, nameof(Unit), builder, context.Units.Local);
            DumpEntitySet(context, nameof(User), builder, context.Users.Local);
            DumpEntitySet(context, nameof(Department), builder, context.Departments.Local);
            DumpEntitySet(context, nameof(Role), builder, context.Roles.Local);

            return builder.ToString();
        }

        private static void DumpEntitySet<T>(BudgetRequestDbContext context, string name, StringBuilder builder, ObservableCollection<T> entities) where T : Entity
        {
            builder.AppendLine($"{name} {entities.Count()}");
            foreach (Entity entity in entities)
            {
                builder.AppendLine($"{entity.Id} {context.Entry(entity).State}");
            }

            builder.AppendLine("----------------------");
        }

    }
}
