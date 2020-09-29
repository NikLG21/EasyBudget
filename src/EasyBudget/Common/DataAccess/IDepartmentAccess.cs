using System;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess
{
    public interface IDepartmentAccess
    {
        void Add(Department department);
        void Update(Department department);
        Department Get(Guid id);
    }
}
