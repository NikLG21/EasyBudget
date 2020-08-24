using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class WrongPasswordException : Exception
    {
        const string FormatMessageString = "Неправильно введен пароль при попытке {0}";
        public string TaskName { get; private set; }
        public WrongPasswordException(string taskName) : base(string.Format(FormatMessageString, taskName))
        {
            TaskName = taskName;
        }
    }
}
