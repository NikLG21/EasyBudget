﻿using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IBudgetRequestViewModel
    {
        BudgetRequest BudgetRequest { get; set; }
        bool IsEditable { get; set; }
        bool InEditMode { get; set; }
    }
}
