using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class NonChangedLoginException:Exception
    {
        const string FormatMessageString = "Логін змінювати не можна.";
        public NonChangedLoginException() : base(FormatMessageString)
        {
        }
    }
}
