using HTyo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTyo.Data
{
    public class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var students = new User[]
            {
            new User{Name="Tapio",UserName="Tapio", Address="Osoite", BillingAddress="Osoite",PhoneNumber="12345",Email="tapio@posti.fi", Password="Salasana"}
            };
            foreach (User s in students)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();
        }
    }
}
