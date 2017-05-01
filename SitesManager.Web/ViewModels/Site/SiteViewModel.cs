using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SitesManager.Web.ViewModels.Site
{
    public class SiteViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название сайта")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Адрес сайта")]
        public string Url { get; set; }


        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        [Display(Name = "Описание")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Статус")]
        public HttpStatusCode? Status { get; set; }

        public string StatusName { get; set; }
    }
}