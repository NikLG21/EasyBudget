using System;
using Common.Model.Security;

namespace Common.Model
{
    public class BudgetDescription : Entity
    {
        private string _description;
        private User _user;
        private DateTime _date;
    }
}
