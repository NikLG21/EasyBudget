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
        public string DisplayName { get; set; }

        //TODO: Add DepartmentId. Done
        public Department Department { get; set; }

        public Guid DepartmentId { get; set; }
        public List<User> Users { get; }
        public List<Action> Actions { get;}
    }
}
