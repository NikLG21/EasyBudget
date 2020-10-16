using System;

namespace EasyBudget.Common.Exceptions
{
    public class WrongPasswordException : Exception
    {
        private const string FormatMessageString = "Неправильно введений пароль.";

        public WrongPasswordException() : this(null)
        {
        }

        public WrongPasswordException(Exception innerException) : base(FormatMessageString, innerException)
        {
        }
    }
}
