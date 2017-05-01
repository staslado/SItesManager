using AutoMapper;
using SitesManager.Data.Models.Site;
using SitesManager.Web.Controllers.Base;
using SitesManager.Web.ViewModels.Site;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SitesManager.Web.Controllers
{
    [Authorize]
    public class SitesController : BaseController
    {
        // GET: Sites
        [AllowAnonymous]
        public ActionResult Index() => View(Mapper.Map<List<SiteViewModel>>(DbContext.Sites.ToList()));

        // GET: Sites/Create
        public ActionResult Create() => View();

        // POST: Sites/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] SiteViewModel site)
        {
            if (ModelState.IsValid)
            {
                var siteEntity = Mapper.Map<SiteEntity>(site);
                DbContext.Sites.Add(siteEntity);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(site);
        }

        // GET: Sites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var siteEntity = DbContext.Sites.Find(id);
            if (siteEntity == null)
                return HttpNotFound();

            return View(Mapper.Map<SiteViewModel>(siteEntity));
        }

        // POST: Sites/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteViewModel site)
        {
            if (ModelState.IsValid)
            {
                var siteEntity = Mapper.Map<SiteEntity>(site);
                DbContext.Entry(siteEntity).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: Sites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var siteEntity = DbContext.Sites.Find(id);
            if (siteEntity == null)
                return HttpNotFound();

            return View(Mapper.Map<SiteViewModel>(siteEntity));
        }

        // POST: Sites/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var siteEntity = DbContext.Sites.Find(id);
            if (siteEntity == null)
                return HttpNotFound();

            DbContext.Sites.Remove(siteEntity);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}