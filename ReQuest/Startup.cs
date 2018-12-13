using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using ReQuest.Models;

[assembly: OwinStartupAttribute(typeof(ReQuest.Startup))]
namespace ReQuest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("Admin"))
            {
                //Creates admin role
                var role = new IdentityRole("Admin");
                roleManager.Create(role);

                //Creating a Admin user who will maintain the website
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                string pwd = "Admin123!@";

                var newUser = userManager.Create(user, pwd);

                //Add default User to Role Admin   
                if (newUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
