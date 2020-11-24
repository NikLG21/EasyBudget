using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Localization
{
    public static class Localization
    {
        private  static Dictionary<BudgetState, string> _budgetStateLocalization = new Dictionary<BudgetState, string>()
        {
            {BudgetState.Requested, "Запрошено"},
            {BudgetState.ApprovedFirstLine,"Затверджено"},
            {BudgetState.ExecutorEstimated,"Встановлений виконавець"},
            {BudgetState.ApprovedDirector,"Затверджено директором"},
            {BudgetState.PostponedDirector,"Призупинено директором"},
            {BudgetState.PostponedFinDirector,"Призупинено фін-директором"},
            {BudgetState.RejectedDirector,"Відмовлено директором"},
            {BudgetState.RejectedFirstLine,"Відмовлено"},
            {BudgetState.Undefined,"-"},
            {BudgetState.Executing,"Виконується"},
            {BudgetState.Executed,"Виконано"}

        };

        public static string GetLocalizationState(this BudgetState state)
        {
            return _budgetStateLocalization[state];
        }
    }
}
