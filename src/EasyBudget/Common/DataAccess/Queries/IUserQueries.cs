using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IUserQueries
    {
        Guid GetUserByLogin(string login, string password);
        List<string> GetUserActions(Guid userId);
    }
}
