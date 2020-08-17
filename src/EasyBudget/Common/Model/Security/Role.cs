using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public Department Department { get; set; }
        public List<User> Users { get;} = new List<User>();
        public List<Action> Actions { get; } = new List<Action>();
    }
}
