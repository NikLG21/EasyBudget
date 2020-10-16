using System;

namespace EasyBudget.Common.Exceptions
{
    public class EntityUpdatedException : Exception
    {
        private const string FormatMessageString = "\"{0}\" в базі був кимось оновлений або видалений. Будь ласка, обновіть дані.";

        public string EntityName { get; }

        public EntityUpdatedException(string entityName) : this(entityName, null)
        {
            EntityName = entityName;
        }

        public EntityUpdatedException(string entityName, Exception innerException) : base(string.Format(FormatMessageString, entityName), innerException)
        {
            EntityName = entityName;
        }
    }
}
