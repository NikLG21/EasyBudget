using System;
using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class User : Entity
    {
        public User(Guid id):base(id)
        {
            Roles = new List<Role>();
        }
        public User()
        {
            Roles = new List<Role>();
        }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsDisabled { get; set; }

        //TODO: Add UnitId. Done
        public Unit Unit { get; set; }

        public Guid? UnitId { get; set; }

        public List<Role> Roles { get;}
    }
}
