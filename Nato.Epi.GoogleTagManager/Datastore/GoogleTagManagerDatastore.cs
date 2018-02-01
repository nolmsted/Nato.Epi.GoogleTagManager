using System;
using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace Nato.Epi.GoogleTagManager.Datastore
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class GoogleTagManagerDatastore
    {
        public Identity Id { get; set; }
        public string AccountKey { get; set; }
        public Guid SiteId { get; set; }
        public string LangId { get; set; }
    }
}