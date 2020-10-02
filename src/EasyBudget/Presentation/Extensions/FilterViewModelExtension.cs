using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.Model;
using EasyBudget.Presentation.ViewModels;

namespace EasyBudget.Presentation.Extensions
{
    public static class FilterViewModelExtension
    {
        public static IEnumerable<BudgetRequestRowViewModel> RequesterFilter(
            this IEnumerable<BudgetRequestRowViewModel> list, Guid requesterId)
        {
            if (requesterId.Equals(Guid.Empty))
            {
                return list;
            }
            else
            {
                return list.Where(br => br.BudgetRequest.RequesterId.Equals(requesterId));
            }
        }
        public static IEnumerable<BudgetRequestRowViewModel> StateFilter(
            this IEnumerable<BudgetRequestRowViewModel> list, BudgetState state)
        {
            if (state.Equals(BudgetState.Undefined))
            {
                return list;
            }
            else
            {
                return list.Where(br => br.BudgetRequest.State.Equals(state));
            }
        }
        public static IEnumerable<BudgetRequestRowViewModel> DepartmentFilter(
            this IEnumerable<BudgetRequestRowViewModel> list, Guid departmentId)
        {
            if (departmentId.Equals(Guid.Empty))
            {
                return list;
            }
            else
            {
                return list.Where(br => br.BudgetRequest.DepartmentId.Equals(departmentId));
            }
        }
        public static IEnumerable<BudgetRequestRowViewModel> UnitFilter(
            this IEnumerable<BudgetRequestRowViewModel> list, Guid unitId)
        {
            if (unitId.Equals(Guid.Empty))
            {
                return list;
            }
            else
            {
                return list.Where(br => br.BudgetRequest.UnitId.Equals(unitId));
            }
        }
    }
}
