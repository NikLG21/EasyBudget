using System.Collections.Generic;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class UserMainInfoDto : Entity
    {
        public UserMainInfoDto()
        {
            Roles = new List<Role>();
        }
        public string Name { get; set; }
        public string Login { get; set; }
        public List<Role> Roles { get; set; }
    }
}
