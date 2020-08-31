using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class LackMandatoryInformation:Exception
    {
        const string FormatMessageString = "Недостатньо обов'язкової інформації : \"{0}\"";
        public string EntityName { get; private set; }
        public LackMandatoryInformation(string entityName)
        {
            EntityName = entityName;
        }

        public LackMandatoryInformation(string entityName, Exception innerException) : base(string.Format(FormatMessageString, entityName), innerException)
        {
            EntityName = entityName;
        }
    }
}
