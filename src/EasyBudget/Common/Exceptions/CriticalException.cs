using System;

namespace EasyBudget.Common.Exceptions
{
    public class CriticalException : Exception
    {
        const string FormatMessageString = "Критичная ошибка. Обратитесь к разработчику.";
        public CriticalException(Exception innerException) : base(FormatMessageString, innerException)
        {
        }
    }
}
