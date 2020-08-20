using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IUserQuries
    {
        Guid GetUserBuLogin(string login, string password);
        List<string> GetUserActions(Guid userId);
    }
}
