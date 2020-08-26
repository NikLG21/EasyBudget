using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class NonDeletedUpdatedRequestException : Exception
    {
        const string FormatMessageString = "Запрос уже был утвержден. \"{0}\" невозможно.";
        public string EntityName { get; private set; }

        public NonDeletedUpdatedRequestException(string entityName)
        {
            EntityName = entityName;
        }

        public NonDeletedUpdatedRequestException(string entityName, Exception innerException) : base(string.Format(FormatMessageString, entityName), innerException)
        {
            EntityName = entityName;
        }
    }
}
