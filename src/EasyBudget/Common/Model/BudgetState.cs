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
        PostponedDirector = 7,
        PostponedFinDirector = 8,
        Executing = 9,
        Executed = 10
    }
}
