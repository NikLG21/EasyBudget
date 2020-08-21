using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class GetNullException:Exception
    {
        const string FormatMessageString = "В базе обьект типа \"{0}\" не найден.";
        public string EntityName { get; private set; }

        public GetNullException(string entityName) : base(string.Format(FormatMessageString, entityName))
        {
            EntityName = entityName;
        }
    }
}
