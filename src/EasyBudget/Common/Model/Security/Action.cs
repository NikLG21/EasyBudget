using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class Action : Entity
    {
        public Action()
        {
            Roles = new List<Role>();
        }

        public string Name { get; set; }
        public List<Role> Roles { get; set; }
    }
}
