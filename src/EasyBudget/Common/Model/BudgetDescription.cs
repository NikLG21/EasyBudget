using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Model
{
    public class BudgetDescription : Entity
    {
        private string _description;
        private User _user;
        private DateTime _date;
    }
}
