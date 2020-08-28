using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class DuplicateEntryException : Exception
    {
        const string FormatMessageString = "Неможливо додати або оновити \"{0}\". Сутність з такими параметрами вже існує.";

        public string EntityName { get; private set; }

        public DuplicateEntryException(string entityName)
        {
            EntityName = entityName;
        }

        public DuplicateEntryException(string entityName, Exception innerException) : base(string.Format(FormatMessageString, entityName), innerException)
        {
            EntityName = entityName;
        }
    }
}
