using System;
using System.Runtime.CompilerServices;

namespace EasyBudget.Common.Model
{
    public class Entity
    {
        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}: {Id:D}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity other)
            {
                return Id == other.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
