using System;
using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class Role : Entity
    {
        public Role()
        {
            Users = new HashSet<User>();
            Actions = new HashSet<Action>();
        }

        public string Name { get; set; }
        public Department Department { get; set; }
        public Guid? DepartmentId { get; set; }
        public HashSet<User> Users { get; set; }
        public HashSet<Action> Actions { get; set; }
    }
}
