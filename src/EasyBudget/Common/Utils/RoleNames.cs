namespace EasyBudget.Common.Utils
{

    //TODO: Please move to the Model/Security folder
    public class RoleNames
    {
        public const string Requestor = "Requestor";
        public const string Approver = "Approver";
        public const string Executor = "Executor";
        //TODO: Bad solution. Please check Executor, than department. Need to drop
        public const string ExecutorIT = "ExecutorIT";
        public const string Director = "Director";
        public const string FinDirector = "FinDirector";
    }
}
