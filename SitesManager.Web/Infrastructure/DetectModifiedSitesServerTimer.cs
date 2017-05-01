using AutoMapper;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SitesManager.Common.Extensions;
using SitesManager.Data;
using SitesManager.Web.Controllers.Hubs;
using SitesManager.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;
using System.Web.Hosting;

namespace SitesManager.Web.Infrastructure
{
    internal class DetectModifiedSitesServerTimer : IRegisteredObject
    {
        private readonly IHubContext _hub;
        private readonly Timer _timer;
        private readonly int _timerInterval;

        public DetectModifiedSitesServerTimer()
        {
            // Get hub context
            _hub = GlobalHost.ConnectionManager.GetHubContext<SitesHub>();

            // Get timer interval from web.config. If value < 10 seconds, then set default value (10s.)
            if (
                !int.TryParse(ConfigurationManager.AppSettings["SitesStatusChangedTimerInterval"], out _timerInterval) ||
                (_timerInterval < 10000))
                _timerInterval = 10000;

            // Create and start timer
            _timer = new Timer(_timerInterval);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        public void Stop(bool immediate)
        {
            _timer.Dispose();
            HostingEnvironment.UnregisterObject(this);
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            _timer.Stop();

            try
            {
                var sites = new List<SiteViewModel>();
                var isNeedUpdateDb = false;

                // Получим все сайты и проверим параллельно их доступность
                using (var dbContext = new ApplicationDbContext())
                {
                    var dbSites = dbContext.Sites.ToList();
                    dbSites.AsParallel().ForAll(x =>
                    {
                        var status = x.Url.GetStatusCode();
                        if (x.Status != status)
                        {
                            x.Status = status;
                            sites.Add(Mapper.Map<SiteViewModel>(x));
                            isNeedUpdateDb = true;
                        }
                    });

                    if (isNeedUpdateDb)
                        dbContext.SaveChanges();
                }

                // Send info about chaтged sites to all clients
                if (sites.Any())
                    _hub.Clients.All.sitesStatusReceived(JsonConvert.SerializeObject(sites));
            }
            catch (Exception ex) // TODO need logging service
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                _timer.Start();
            }
        }
    }
}