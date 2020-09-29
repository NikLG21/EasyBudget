using System;

namespace EasyBudget.Common.Exceptions
{
    public class WrongPasswordException : Exception
    {
        private const string FormatMessageString = "Неправильно введений пароль.";

        public WrongPasswordException() : base(FormatMessageString)
        {
        }
    }
}
