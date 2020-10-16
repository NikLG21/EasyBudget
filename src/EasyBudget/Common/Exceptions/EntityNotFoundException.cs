using System;

namespace EasyBudget.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private const string FormatMessageString = "У базі об'єкт типу \"{0}\" не знайдений.";

        public string EntityName { get; }

        public EntityNotFoundException(string entityName) : this(entityName, null)
        {
            EntityName = entityName;
        }

        public EntityNotFoundException(string entityName, Exception innerException) : base(string.Format(FormatMessageString, entityName), innerException)
        {
            EntityName = entityName;
        }
    }
}
