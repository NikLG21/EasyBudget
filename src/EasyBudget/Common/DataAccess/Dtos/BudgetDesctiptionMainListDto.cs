﻿using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class BudgetDesctiptionMainListDto : Entity
    {
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}
