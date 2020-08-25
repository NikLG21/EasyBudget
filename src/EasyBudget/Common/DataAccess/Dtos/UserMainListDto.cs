using System.Collections.Generic;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class UserMainListDto
    {
        public UserMainListDto()
        {
            Roles = new List<Role>();
        }
        public string Name { get; set; }
        public string Login { get; set; }
        public List<Role> Roles { get; set; }
    }
}
