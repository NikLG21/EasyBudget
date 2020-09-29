using System;

namespace EasyBudget.Common.Exceptions
{
    public class CriticalException : Exception
    {
        private const string FormatMessageString = "Критична помилка. Зверніться до розробника.";

        public CriticalException(Exception innerException) : base(FormatMessageString, innerException)
        {
        }
    }
}
