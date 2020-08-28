using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class DisabledUserException:Exception
    {
        const string FormatMessageString = "Цей користувач був відключений.";
        public DisabledUserException() : base(FormatMessageString)
        {
        }
    }
}
