using System;

namespace EasyBudget.Common.Exceptions
{
    public class NonDeletedUpdatedRequestException : Exception
    {
        private const string FormatMessageString = "Запит вже був затверджений. \"{0}\" неможливо.";

        public string Details { get; }

        public NonDeletedUpdatedRequestException(string details)
        {
            Details = details;
        }

        public NonDeletedUpdatedRequestException(string details, Exception innerException) : base(string.Format(FormatMessageString, details), innerException)
        {
            Details = details;
        }
    }
}
