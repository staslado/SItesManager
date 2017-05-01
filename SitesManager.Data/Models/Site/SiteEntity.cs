using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SitesManager.Data.Models.Site
{
    public class SiteEntity
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public HttpStatusCode? Status { get; set; }
    }
}