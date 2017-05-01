using AutoMapper;
using SitesManager.Data.Models.Site;
using SitesManager.Web.ViewModels.Site;
using System;
using System.Net;

namespace SitesManager.Web
{
    public static class MapperConfig
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<SiteEntity, SiteViewModel>()
                    .ForMember(x => x.StatusName, x => x.MapFrom(t => t.Status.HasValue ? Enum.GetName(typeof(HttpStatusCode), t.Status.Value) : "Не определен"));
                config.CreateMap<SiteViewModel, SiteEntity>();
            });
        }
    }
}