using System;

namespace EasyBudget.Common.Exceptions
{
    public class UnityInUserRequiredException : Exception
    {
        private const string FormatMessageString = "Користувачі з ролями \"Ініціатор запиту\" та \"Затверджувач\" можуть та повинні мати підрозділ.";

        public UnityInUserRequiredException() : this(null)
        {
        }

        public UnityInUserRequiredException(Exception innerException) : base(FormatMessageString, innerException)
        {
        }
    }
}
