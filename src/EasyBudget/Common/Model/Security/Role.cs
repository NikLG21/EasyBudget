using System;
using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class Role : Entity
    {
        public Role()
        {
            Users = new List<User>();
            Actions = new List<Action>();
        }

        public string Name { get; set; }
        public Department? Department { get; set; }
        public List<User> Users { get; set; }
        public List<Action> Actions { get; set; }
    }
}
