using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IDepartmentAccess
    {
        void Add(Department department);
        void Update(Department department);
        void Delete(Guid id);
        Department Get(Guid id);
    }
}
