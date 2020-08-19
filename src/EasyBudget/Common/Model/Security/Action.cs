using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class Action : Entity
    {
        public Action()
        {
            Roles = new HashSet<Role>();
        }

        public string Name { get; set; }
        public HashSet<Role> Roles { get; set; }
    }
}
