using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Exceptions
{
    public class NonDeletedBudgetRequestException : Exception
    {
        const string FormatMessageString = "Запрос уже был утвержден. Удаление невозможно.";
        public NonDeletedBudgetRequestException() : base(FormatMessageString)
        {
        }
    }
}
