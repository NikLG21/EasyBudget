﻿using System.Collections.Generic;

namespace EasyBudget.Common.Model.Security
{
    public class User : Entity
    {
        public string Name { get; set; }
        public List<Role> Roles { get; } = new List<Role>();
    }
}
