using System;

namespace EasyBudget.Presentation.Utils
{
    public class PairGuid
    {
        public PairGuid(Guid key, string value)
        {
            Key = key;
            Value = value;
        }

        public Guid Key { get; }
        public string Value { get; }

        public override bool Equals(object obj)
        {
            PairGuid pairGuid = (PairGuid) obj;
            if (pairGuid.Key.Equals(Key) && pairGuid.Value.Equals(Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() * Value.GetHashCode();
        }
    }
}
