using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void AddUserByAdmin(User user);
        Guid LogInUser( string login, string password);
        void ChangePasswordByUser( Guid userId, string oldPassword, string newPassword);
        void UpdateByAdmin(User user);
        List<UserMainInfoDto> GetUsersList();
        UserMainInfoDto GetMainInfoDto( Guid id);
    }
}
