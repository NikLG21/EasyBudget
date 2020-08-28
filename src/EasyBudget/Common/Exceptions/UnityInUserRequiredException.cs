using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class UnityInUserRequiredException : Exception
    {
        const string FormatMessageString = "Тільки користувачі з ролями \"Ініціатор запиту\" або \"Затверджувач\" можуть та повинні мати Unity.";
        public UnityInUserRequiredException() : base(FormatMessageString)
        {
        }
    }
}
