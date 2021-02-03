using System;

namespace EasyBudget.Common.Model
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }

        //TODO: remove setter
        public Guid Id { get; }

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
