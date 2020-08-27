using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void Add(User user);
        Guid LogIn( string login, string password);
        void ChangePassword( Guid userId, string oldPassword, string newPassword);
        void UpdateByAdmin(User user);
        List<UserMainInfoDto> GetUsersList();
        UserMainInfoDto GetMainInfoDto( Guid id);
    }
}
