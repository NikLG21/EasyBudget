using System;

namespace EasyBudget.Common.Model
{
    public class Department : Entity
    {
        public Department()
        {
        }

        public Department(Guid id) : base(id)
        {
        }

        public string Name { get; set; }
    }
}
