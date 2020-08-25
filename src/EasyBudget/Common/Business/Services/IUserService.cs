using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void Add(Guid currentUserId,User user);
        Guid LogIn(Guid currentUserId, string login, string password);
        void UpdatePassword(Guid currentUserId, Guid userId, string oldPassword, string newPassword);
        void Update(Guid currentUserId,User user);
        List<UserMainInfoDto> GetUsersList(Guid currentUserId);
        UserMainInfoDto GetMainInfoDto(Guid currentUserId, Guid id);
    }
}
