using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        /* */

        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            /* إستدعءا الدالة المنشئة تحت */

            CreateDefaultRolesAndUsers(); 

            /* */
        }

        /* */ 
        public void CreateDefaultRolesAndUsers()
        {
            /* إدارة صلاحيات الدخول */ 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            /* إدارة المستخدمين */
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            /* لـ إنشاء صلاحية معينه برمجياً */ 
            IdentityRole role = new IdentityRole();
            /* يتحقق من الصلاحيه لو مش موجوده يتم إنشاءها */
            if(!roleManager.RoleExists("Admins"))
            {
                role.Name = "Admins";
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser();
                user.UserName = "ESlAM";
                user.Email = "eslam@live.fr";

                var chek = UserManager.Create(user, "Es@1!d1600");

                if(chek.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admins");
                }
            }

        }

        /* */
    }
}
