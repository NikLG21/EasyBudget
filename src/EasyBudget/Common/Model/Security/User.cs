using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class User : Entity
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public HashSet<Role> Roles { get; set; }
    }
}
