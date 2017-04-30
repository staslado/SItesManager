using System;
using System.ComponentModel.DataAnnotations;

namespace SitesManager.Data.Models.Site
{
    public class SiteEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public SiteStatus Status { get; set; }

        public DateTime LastModifiedTime { get; set; }
    }
}