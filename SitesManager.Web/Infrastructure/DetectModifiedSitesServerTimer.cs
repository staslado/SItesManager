using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;
using System.Web.Hosting;
using AutoMapper;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SitesManager.Data;
using SitesManager.Web.Controllers.Hubs;
using SitesManager.Web.ViewModels.Site;

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

            // Get timer interval from web.config. If value < 3 seconds, then set default value (15s.)
            if (
                !int.TryParse(ConfigurationManager.AppSettings["SitesStatusChangedTimerInterval"], out _timerInterval) ||
                (_timerInterval < 3000))
                _timerInterval = 15000;

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
            var sites = new List<SiteViewModel>();

            // Получим все сайты, в которых произошли какие-либо изменения за время прошедшее с начала запуска таймера
            using (var dbContext = new ApplicationDbContext())
            {
                var changeDateTime = DateTime.Now.AddMilliseconds(-_timerInterval);
                var changedSites = dbContext.Sites.Where(x => x.LastModifiedTime >= changeDateTime).ToList();
                sites.AddRange(Mapper.Map<List<SiteViewModel>>(changedSites));
            }

            // Send info about chamged sites to all clients
            _hub.Clients.All.sitesStatusReceived(JsonConvert.SerializeObject(sites));
        }
    }
}