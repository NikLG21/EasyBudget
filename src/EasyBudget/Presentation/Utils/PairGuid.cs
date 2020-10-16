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
    }
}
