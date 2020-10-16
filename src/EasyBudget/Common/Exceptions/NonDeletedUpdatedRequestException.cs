using System;

namespace EasyBudget.Common.Exceptions
{
    public class NonDeletedUpdatedRequestException : Exception
    {
        private const string FormatMessageString = "Запит вже був затверджений. Зміни неможливі.";

        public NonDeletedUpdatedRequestException() : this(null)
        {
        }

        public NonDeletedUpdatedRequestException(Exception innerException) : base(FormatMessageString, innerException)
        {
        }
    }
}
