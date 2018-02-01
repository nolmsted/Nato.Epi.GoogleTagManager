using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EPiServer.Data;

namespace Nato.Epi.GoogleTagManager.ViewModels
{
    public class GoogleTagManagerAdminViewModel
    {
        public List<SiteWithGtm> Sites { get; set; }

        [Required]
        public string SiteId { get; set; }        
                
        public string AccountKey { get; set; }

        public string LangId { get; set; }

        public string DatastoreId { get; set; }

        public GoogleTagManagerAdminViewModel()
        {            
            Sites = new List<SiteWithGtm>();
        }        
    }

    public struct SiteWithGtm
    {
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }
        public string LangId { get; set; }
        public string LangName { get; set; }
        public Identity DatastoreId { get; set; }
        public string DatastoreAccountKey { get; set; }
        public List<SiteWithGtm> LangSites { get; set; }

        public SiteWithGtm(Guid siteId, string siteName, string langId = null, string langName = null) : this()
        {            
            SiteId = siteId;
            SiteName = siteName;
            LangId = langId;
            LangName = langName;
            LangSites = new List<SiteWithGtm>();
        }
    }
}