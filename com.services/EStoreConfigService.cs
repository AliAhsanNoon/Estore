using System.Linq;
using com.database;
using com.Entities;

namespace com.services
{
    public class EStoreConfigService
    {
        public EStoreConfiguration GetConfigurationSettings(string key)
        {
            using (var context = new CContext())
            {
                return context.EStoreConfigurations.SingleOrDefault(x => x.Key.Contains(key));
            }
        }
    }
}
