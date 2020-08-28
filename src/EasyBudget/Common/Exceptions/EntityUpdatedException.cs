using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class EntityUpdatedException:Exception
    {
        const string FormatMessageString = "\"{0}\" в базі був кимось оновлений. Обновите \"{0}\" у себе в клієнті.";
        public string EntityName { get; private set; }

        public EntityUpdatedException(string entityName)
        {
            EntityName = entityName;
        }

        public EntityUpdatedException(string entityName, Exception innerException) : base(string.Format(FormatMessageString, entityName), innerException)
        {
            EntityName = entityName;
        }
    }
}
