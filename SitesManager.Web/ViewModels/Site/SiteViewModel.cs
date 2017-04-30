using System;
using System.ComponentModel.DataAnnotations;
using SitesManager.Data.Models.Site;

namespace SitesManager.Web.ViewModels.Site
{
    public class SiteViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название сайта")]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        [Display(Name = "Описание")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Статус")]
        public SiteStatus Status { get; set; }

        [Display(Name = "Дата последнего изменения")]
        public DateTime LastModifiedTime { get; set; }
    }
}