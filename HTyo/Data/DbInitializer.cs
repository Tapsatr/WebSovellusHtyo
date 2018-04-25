using HTyo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Data
{
    public class DbInitializer
    {
        public static void Initialize(UserContext context, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var students = new User[]
            {
            new User{Name="Tapio",UserName="Tapio", Address="Osoite", BillingAddress="Osoite",PhoneNumber="12345",Email="tapio@posti.fi", Password="Salasana12!"},
            new User{Name="Tapio2",UserName="Tapio2", Address="Osoite2", BillingAddress="Osoite2",PhoneNumber="12345",Email="tapio2@posti.fi", Password="Salasana12!"}
            };
            foreach (User s in students)
            {
                var result = userManager.CreateAsync(s, s.Password).Result;
            }
            var orders = new JobOrder[]
            {
                new JobOrder{Orderer="Tapio", AcceptedOrderDate=DateTime.Parse("2018-05-01"), OrderDate=DateTime.Parse("2017-12-22"),
                    JobDescription ="Fix bathroom", JobComment="Easy job", ReadyDate=DateTime.Parse("2018-05-10"), ToolsComment="Hammer",
                    HoursOnJob =250, StartDate=DateTime.Parse("2018-05-02"), Status= Status.READY },
                     new JobOrder{Orderer="Tapio", AcceptedOrderDate=DateTime.Parse("2018-05-01"), OrderDate=DateTime.Parse("2017-12-22"),
                    JobDescription ="Fix kitchen", JobComment="Hard job", ReadyDate=DateTime.Parse("2018-05-10"), ToolsComment="Drill",
                    HoursOnJob =250, StartDate=DateTime.Parse("2018-05-02"), Status= Status.READY },
                    new JobOrder{Orderer="Tapio2", RejectedOrderDate=DateTime.Parse("2018-05-01"), OrderDate=DateTime.Parse("2017-12-22"),
                    JobDescription ="Mow lawn", JobComment="no lawn",
                    Status= Status.REJECTED }
            };
            foreach (JobOrder j in orders)
            {
                context.JobOrders.Add(j);
            }
            context.SaveChanges();
        }
    }
}
