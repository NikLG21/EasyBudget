using System;

namespace EasyBudget.Common.Exceptions
{
    public class DuplicateEntryException : Exception
    {
        private const string FormatMessageString = "Неможливо додати або оновити \"{0}\". Сутність з такими параметрами вже існує.";

        public string EntityName { get; }

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
