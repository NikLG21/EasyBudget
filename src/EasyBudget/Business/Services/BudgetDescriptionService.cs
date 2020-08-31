using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services
{
    public class BudgetDescriptionService : IBudgetDescriptionService
    {
        private IBudgetDescriptionQueries budgetDescriptionQueries;
        private IBudgetDescriptionAccess budgetDescriptionAccess;
        private IUserAccess userAccess;
        public void AddBudgetDescription(Guid userId, BudgetDescription description)
        {
            try
            {
                if (description.Description == null)
                {
                    throw new LackMandatoryInformation("Коментар");
                }

                if (description.BudgetRequest == null)
                {
                    throw new LackMandatoryInformation("Запиту до якого відноситься коментар");
                }
                description.User = userAccess.Get(userId);
                description.Date = DateTime.Today;
                budgetDescriptionAccess.Add(description);
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public BudgetDescription GetBudgetDescription(Guid userId, Guid descriptionId)
        {
            try
            {
                return budgetDescriptionAccess.Get(descriptionId);
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public List<BudgetDescriptionMainListDto> GetBudgetDescriptionListByRequest(Guid userId, Guid budgetRequestId)
        {
            try
            {
                return budgetDescriptionQueries.GetBudgetDescriptionByRequest(budgetRequestId);
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }
    }
}
