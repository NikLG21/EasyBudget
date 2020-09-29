using System;

namespace EasyBudget.Common.Exceptions
{
    public class DisabledUserException : Exception
    {
        private const string FormatMessageString = "Цей користувач був відключений.";

        public DisabledUserException() : base(FormatMessageString)
        {
        }
    }
}
