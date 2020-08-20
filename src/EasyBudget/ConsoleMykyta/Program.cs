using System;
using DataAccess;
using DataAccess.Access;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Commands;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace ConsoleMykyta
{
    class Program
    {
        static void Main(string[] args)
        {
            IDepartmentAccess departmentAccess = new DepartmentAccess();
            Department department = new Department()
            {
                Id = Guid.NewGuid(),
                Name = "My dept"
            };
            departmentAccess.Add(department);
            return;

            IBudgetRequestAccess access = new BudgetRequestAccess();
            BudgetRequest request = new BudgetRequest()

            {
                Id = Guid.NewGuid(),
                Name = "Class",
                DateRequested = DateTime.Now,
                DateRequestedDeadline = DateTime.MaxValue,
                DateDirectorApprove = DateTime.Today,
                DateStartExecution = DateTime.Now,
                DateDeadlineExecution = DateTime.Now,
                DateEndExecution = DateTime.Now,
                RealPrice = 100,
                EstimatedPrice = 120,
                State = BudgetState.ApprovedDirector,
                //BudgetDescriptions =
                //{
                //    new BudgetDescription()
                //    {
                //        Date = DateTime.Today,
                //        Description = String.Empty,
                //        Id = Guid.NewGuid(),
                //        User = new User()
                //        {
                //            Id = Guid.NewGuid(),
                //            Login = "log",
                //            Name = "g",
                //            Password = "dfd"
                //        }
                //    }
                //}
                
            };

            access.Add(request);
            //Guid id = request.Id;
            //BudgetRequest budgetRequest = access.Get(id);
            //Console.WriteLine(budgetRequest.Id);
        }
    }
}
