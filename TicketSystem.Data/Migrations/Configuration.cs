namespace TicketSystem.Data.Migrations
{
    using TicketSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.ObjectModel;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.RegisterUserAdmin(context);
            this.GenerateData(context);
        }

        private void RegisterUserAdmin(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var userAdmin = new ApplicationUser()
            {
                UserName = "admin",
                Logins = new Collection<UserLogin>()
                {
                    new UserLogin()
                    {
                        LoginProvider = "Local",
                        ProviderKey = "admin",
                    }
                },
                Roles = new Collection<UserRole>()
                {
                    new UserRole()
                    {
                        Role = new Role("Admin")
                    }
                }
            };

            context.Users.Add(userAdmin);
            context.UserSecrets.Add(new UserSecret("admin",
                "ACQbq83L/rsvlWq11Zor2jVtz2KAMcHNd6x1SN2EXHs7VuZPGaE8DhhnvtyO10Nf5Q=="));//admin123
            context.SaveChanges();
        }

        private void GenerateData(ApplicationDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName == "admin");
            for (int i = 1; i <= 5; i++)
			{
                Category category = new Category()
                {
                    Name = "Category" + i
                };

                for (int j = 1; j <= 10; j++)
                {
                    Ticket ticket = new Ticket()
                    {
                        Author = user,
                        Title = "Title" + j,
                        CategoryId = i                      
                    };

                    category.Tickets.Add(ticket);
                    context.Tickets.Add(ticket);
                }

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }
    }
}
