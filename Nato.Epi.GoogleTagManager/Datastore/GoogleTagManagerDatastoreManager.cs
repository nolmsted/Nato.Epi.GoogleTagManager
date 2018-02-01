using System.Collections.Generic;
using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace Nato.Epi.GoogleTagManager.Datastore
{
    internal class GoogleTagManagerDatastoreManager
    {
        internal GoogleTagManagerDatastore GetStoreById(Identity id)
        {
            using (var store = typeof(GoogleTagManagerDatastore).GetOrCreateStore())
            {
                return store.Load<GoogleTagManagerDatastore>(id);
            }
        }

        internal IEnumerable<GoogleTagManagerDatastore> GetAllGtmData()
        {
            using (var store = typeof(GoogleTagManagerDatastore).GetOrCreateStore())
            {
                return store.LoadAll<GoogleTagManagerDatastore>();
            }
        }

        internal Identity SaveGtmData(GoogleTagManagerDatastore gtmData)
        {
            if (gtmData.SiteId == null)
            {
                return null;
            }
            using (var store = typeof(GoogleTagManagerDatastore).GetOrCreateStore())
            {
                return gtmData.Id == null ? store.Save(gtmData) : store.Save(gtmData, gtmData.Id);
            }
        }

        internal void DeleteGtmData(GoogleTagManagerDatastore gtmData)
        {
            using (var store = typeof(GoogleTagManagerDatastore).GetOrCreateStore())
            {
                store.Delete(gtmData.Id);
            }
        }
    }
}