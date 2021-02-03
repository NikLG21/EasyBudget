using System;

namespace EasyBudget.Common.Model
{
    public class Unit : Entity
    {
        public Unit()
        {
        }

        public Unit(Guid id) : base(id)
        {
        }

        public string Name { get; set; }
    }
}
