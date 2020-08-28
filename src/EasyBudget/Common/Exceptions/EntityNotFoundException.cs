using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class EntityNotFoundException:Exception
    {
        const string FormatMessageString = "У базі об'єкт типу \"{0}\" не знайдений.";
        public string EntityName { get; private set; }

        public EntityNotFoundException(string entityName) : base(string.Format(FormatMessageString, entityName))
        {
            EntityName = entityName;
        }
    }
}
