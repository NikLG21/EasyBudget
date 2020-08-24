using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class DisabledUserException:Exception
    {
        const string FormatMessageString = "Этот пользователь был отключен.";
        public DisabledUserException() : base(FormatMessageString)
        {
        }
    }
}
