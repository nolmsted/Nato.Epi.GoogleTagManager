using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Data;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Web;
using Nato.Epi.GoogleTagManager.Datastore;
using Nato.Epi.GoogleTagManager.ViewModels;

namespace Nato.Epi.GoogleTagManager.Controllers
{
    [GuiPlugIn(
        Area = PlugInArea.AdminConfigMenu,
        Url = "/modules/Nato.Epi.GoogleTagManager/GoogleTagManagerAdmin",
        DisplayName = "Google Tag Manager Configuration",
        SortIndex = 0
        )]
    public class GoogleTagManagerAdminController : Controller
    {
        private readonly ISiteDefinitionRepository siteDefinitionRepo;
        private readonly ILanguageBranchRepository languageBranchRepo;

        public GoogleTagManagerAdminController(ISiteDefinitionRepository _siteDefinitionRepo, ILanguageBranchRepository _languageBranchRepo)
        {
            siteDefinitionRepo = _siteDefinitionRepo;
            languageBranchRepo = _languageBranchRepo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new GoogleTagManagerAdminViewModel();            
            var mgr = new GoogleTagManagerDatastoreManager();

            var langs = languageBranchRepo.ListEnabled();
            var datastoreItems = mgr.GetAllGtmData();
            foreach(var siteDef in siteDefinitionRepo.List())
            {
                SiteWithGtm site = new SiteWithGtm(siteDef.Id, siteDef.Name);
                var datastoreItem = datastoreItems.Where(x => x.SiteId.Equals(site.SiteId)).FirstOrDefault();
                if (datastoreItem != null)
                {
                    site.DatastoreId = datastoreItem.Id;
                    site.DatastoreAccountKey = datastoreItem.AccountKey;
                }
                
                //get entry for each language enabled if more than one
                if(langs.Count > 1)
                {
                    foreach (var lang in langs)
                    {
                        SiteWithGtm langSite = new SiteWithGtm(siteDef.Id, siteDef.Name, lang.LanguageID, lang.Name);
                        var dsItem = datastoreItems.Where(x => x.SiteId.Equals(langSite.SiteId) && x.LangId == langSite.LangId).FirstOrDefault();
                        if (dsItem != null)
                        {
                            langSite.DatastoreId = dsItem.Id;
                            langSite.DatastoreAccountKey = dsItem.AccountKey;
                        }
                        site.LangSites.Add(langSite);
                    }
                }                

                model.Sites.Add(site);
            }
            
            return View("/modules/_protected/Nato.Epi.GoogleTagManager/Views/GoogleTagManagerAdmin/Index.cshtml", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(GoogleTagManagerAdminViewModel gtmConfig)
        {
            var g = new GoogleTagManagerDatastore();

            //id can be null if it's a new entry
            Identity.TryParse(gtmConfig.DatastoreId, out Identity id);

            if (ModelState.IsValid && Guid.TryParse(gtmConfig.SiteId, out Guid siteId))
            {
                var ds = new GoogleTagManagerDatastore() { Id = id, SiteId = siteId, LangId = gtmConfig.LangId, AccountKey = gtmConfig.AccountKey };
                var gtm = new GoogleTagManagerDatastoreManager();
                var result = gtm.SaveGtmData(ds);
                if (result != null)
                {
                    TempData["status"] = string.Concat("Account Key saved.");
                }
            }
            return RedirectToAction("Index");
        }
    }
}