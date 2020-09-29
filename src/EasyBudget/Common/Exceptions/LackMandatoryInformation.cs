using System;

namespace EasyBudget.Common.Exceptions
{
    public class LackMandatoryInformation : Exception
    {
        private const string FormatMessageString = "Недостатньо обов'язкової інформації: \"{0}\"";

        public string Details { get; }

        public LackMandatoryInformation(string details)
        {
            Details = details;
        }

        public LackMandatoryInformation(string details, Exception innerException) : base(string.Format(FormatMessageString, details), innerException)
        {
            Details = details;
        }
    }
}
