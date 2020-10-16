using System;

namespace EasyBudget.Common.Exceptions
{
    public class UnityInUserRequiredException : Exception
    {
        private const string FormatMessageString = "Тільки користувачі з ролями \"Ініціатор запиту\" або \"Затверджувач\" можуть та повинні мати підрозділ.";

        public UnityInUserRequiredException() : this(null)
        {
        }

        public UnityInUserRequiredException(Exception innerException) : base(FormatMessageString, innerException)
        {
        }
    }
}
