using System;

namespace EasyBudget.Common.Model
{
    public class Entity
    {
        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}: {Id:D}";
        }
    }
}
