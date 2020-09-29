using System;

namespace EasyBudget.Common.Exceptions
{
    public class NonChangedLoginException : Exception
    {
        private const string FormatMessageString = "Логін змінювати не можна.";

        public NonChangedLoginException() : base(FormatMessageString)
        {
        }
    }
}
