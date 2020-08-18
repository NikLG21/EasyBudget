using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Model
{
    public enum BudgetState
    {
        Undefined = 0,
        Requested = 1,
        ExecutorEstimated = 2,
        ApprovedFirstLine = 3,
        ApprovedDirector = 4,
        RejectedFirstLine = 5,
        RejectedDirector = 6,
        PostpondDirector = 7,
        PostpondFinDirector = 8,
        Execution = 9,
        Executed = 10
    }
}
