namespace EasyBudget.Common.Model.Security
{
    public static class  ActionTypes
    {
        //TODO: Could you put verb into past. E.g. RequestorAddedBudgetRequest
        public const string RequestorAddBudgetRequest = "RequestorAddBudgetRequest";
        public const string RequestorUpdateBudgetRequest = "RequestorUpdateBudgetRequest";
        public const string RequestorDeleteBudgetRequest = "RequestorDeleteBudgetRequest";
        public const string ApproverApproveBudgetRequest = "ApproverApproveBudgetRequest";
    }
}
