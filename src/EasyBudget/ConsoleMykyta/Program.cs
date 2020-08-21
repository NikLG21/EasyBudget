using System;
using System.Collections.Generic;
using DataAccess;
using DataAccess.Access;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace ConsoleMykyta
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserAccess userAccess = new UserAccess();
            User user;
            //User user = new User()
            //{
            //    Id = Guid.Parse("76C21A2E-4ECB-4724-ACED-9948CB106702"),
            //    IsDisabled = false,
            //    Login = "Chipolino",
            //    Name = "Григорий Сковорода",
            //    Password = "skovoroda01",
            //    Roles = new List<Role>()
            //};
            user = userAccess.Get(Guid.Parse("2148CE2C-540E-4CC4-A372-42E0C29A478B"));
            Console.WriteLine(user.Name);
            //IDepartmentAccess departmentAccess = new DepartmentAccess();
            //Department department = new Department()
            //{
            //    Id = Guid.Parse("DF8E3D8F-4B48-4489-AAA6-53348267394A"),
            //    Name = "Хозчасть"
            //};
            //departmentAccess.Delete(Guid.Parse("DF8E3D8F-4B48-4489-AAA6-53348267394A"));
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
