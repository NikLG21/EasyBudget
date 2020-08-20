using System;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess
{
    public interface IUserAccess
    {
        void Add(User user);
        void Update(User user);
        User Get(Guid id);
    }
}
