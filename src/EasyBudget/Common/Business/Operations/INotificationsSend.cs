namespace EasyBudget.Common.Business.Operations
{
    public interface INotificationsSend
    {
        void SendApproveNotification();
        void SendDeclineNotification();
        void SendNewBudgetRequestNotification();
    }
}
