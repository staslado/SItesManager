using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SitesManager.Data.Models.Account;
using SitesManager.Data.Models.Site;

namespace SitesManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<SiteEntity> Sites { get; set; }
    }
}