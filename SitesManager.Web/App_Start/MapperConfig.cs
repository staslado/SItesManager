using AutoMapper;
using SitesManager.Data.Models.Site;
using SitesManager.Web.ViewModels.Site;

namespace SitesManager.Web
{
    public static class MapperConfig
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<SiteEntity, SiteViewModel>();
                config.CreateMap<SiteViewModel, SiteEntity>();
            });
        }
    }
}