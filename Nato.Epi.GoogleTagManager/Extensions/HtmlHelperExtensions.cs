using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Nato.Epi.GoogleTagManager.Datastore;

namespace Nato.Epi.GoogleTagManager.Extensions
{
    public static class HtmlHelperExtensions
    {
        private static Injected<ILanguageBranchRepository> languageBranchRepository;

        private const string VIEWPATH = "~/modules/_protected/Nato.Epi.GoogleTagManager/Views/";
        private const string GTM_SCRIPT_VIEW = "GtmScript.cshtml";
        private const string GTM_TAG_VIEW = "GtmTag.cshtml";        

        public static MvcHtmlString RenderGtmScript(this HtmlHelper html)
        {
            return LoadGtmView(html, GTM_SCRIPT_VIEW);
        }

        public static MvcHtmlString RenderGtmTag(this HtmlHelper html)
        {
            return LoadGtmView(html, GTM_TAG_VIEW);
        }

        private static MvcHtmlString LoadGtmView(HtmlHelper html, string viewName)
        {
            var output = new MvcHtmlString(string.Empty);

            //check if in edit mode            
            if(!PageEditing.PageIsInEditMode)
            {                
                var lang = (languageBranchRepository.Service.ListEnabled().Count > 1) ? ContentLanguage.PreferredCulture.Name : null;

                var gtmManager = new GoogleTagManagerDatastoreManager();

                //try to get a datastore entry matching the current site and language. 
                //filter out empty account key properites to handle cleared entries.                
                var gtmData = gtmManager.GetAllGtmData().Where(x => x.SiteId.Equals(SiteDefinition.Current.Id) 
                        && x.LangId == lang 
                        && !string.IsNullOrWhiteSpace(x.AccountKey))
                    .DefaultIfEmpty(gtmManager.GetAllGtmData().Where(x => x.SiteId.Equals(SiteDefinition.Current.Id)
                        && x.LangId == null).FirstOrDefault()).FirstOrDefault();

                if (gtmData != null && !String.IsNullOrWhiteSpace(gtmData.AccountKey))
                {
                    output = html.Partial(string.Concat(VIEWPATH, viewName), gtmData.AccountKey);
                }
            }            
            return output;
        }
    }
}
