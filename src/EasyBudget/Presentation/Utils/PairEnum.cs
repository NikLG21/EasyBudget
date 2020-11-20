using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Presentation.Utils
{
    public class PairEnum<T> where T : struct
    {
        public PairEnum(T key, string value)
        {
            Key = key;
            if (string.IsNullOrEmpty(value))
            {
                Value = key.ToString();
            }
            else
            {
                Value = value;
            }
        }

        public T Key { get; }
        public string Value { get; }

        public override bool Equals(object obj)
        {
            PairEnum<T> pairEnum = (PairEnum<T>)obj;
            if (pairEnum.Key.Equals(Key) && pairEnum.Value == Value)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() * Key.GetHashCode();
        }
    }

}
