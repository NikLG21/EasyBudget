using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;
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

        public static IEnumerable<BudgetRequestRowViewModel> OnGoingFilter(this IEnumerable<BudgetRequestRowViewModel> list, Role role, UserMainInfoDto userInfo)
        {
            switch (role.Name)
            {
                case RoleNames.Approver:
                    return list.Where(br => br.BudgetRequest.UnitId.Equals(userInfo.UnitId)
                                            &&br.BudgetRequest.State.Equals(BudgetState.Requested))
                        .ToList();
                case RoleNames.Director:
                    return list.Where(br => br.BudgetRequest.State.Equals(BudgetState.PostponedDirector) 
                                            |br.BudgetRequest.State.Equals(BudgetState.ExecutorEstimated))
                        .ToList();
                case RoleNames.FinDirector:
                    return list.Where(br => br.BudgetRequest.State.Equals(BudgetState.ApprovedDirector)
                                            | br.BudgetRequest.State.Equals(BudgetState.PostponedFinDirector))
                        .ToList();
                case RoleNames.Executor:
                    return list.Where(br => br.BudgetRequest.DepartmentId.Equals(userInfo.DepartmentId)
                                            && (br.BudgetRequest.State.Equals(BudgetState.ApprovedFirstLine)| br.BudgetRequest.State.Equals(BudgetState.Executing)))
                        .ToList();
                case RoleNames.Requester:
                    return list.Where(br => br.BudgetRequest.State.Equals(BudgetState.Requested))
                        .ToList();
                default:
                    return list;
            }
        }

        public static IEnumerable<BudgetRequestRowViewModel> SelectedRowsFilter(this IEnumerable<BudgetRequestRowViewModel> list)
        {
            return list.Where(br => br.IsSelected.Equals(true));
        }

        public static IEnumerable<BudgetRequestRowViewModel> DateFilter(this IEnumerable<BudgetRequestRowViewModel> list, DateTime from, DateTime to)
        {
            return list.Where(br => br.BudgetRequest.DateRequested >= from && br.BudgetRequest.DateRequested <= to);
        }
    }
}
