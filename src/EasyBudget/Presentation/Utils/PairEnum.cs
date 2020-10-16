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
    }

}
