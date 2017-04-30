using System.ComponentModel.DataAnnotations;

namespace SitesManager.Data.Models.Site
{
    public enum SiteStatus
    {
        [Display(Name = "Недоступен")]
        Disable,
        [Display(Name = "Активен")]
        Active
    }
}