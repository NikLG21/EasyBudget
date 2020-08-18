using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Business.Services
{
    public interface INotificationsSend
    {
        void SendApproveNotification();
        void SendDeclineNotification();
        void SendNewBudgetRequestNotification();
    }
}
