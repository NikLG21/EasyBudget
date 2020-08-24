﻿using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IUserQueries
    {
        Guid GetUserByLogin(string login, string password);
        List<string> GetUserActions(Guid userId);
        List<UserMainListDto> GetUsers();
        UserMainListDto GetMainInfo(Guid id);
    }
}
