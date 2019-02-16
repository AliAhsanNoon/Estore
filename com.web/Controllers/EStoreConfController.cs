using com.Entities;
using com.services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class EStoreConfController : Controller
    {
        EStoreConfigService eConfigService = new EStoreConfigService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EStoreAppConfigurations(EStoreConfiguration configuration)
        {
            //var appKey = eConfigService.GetConfigurationSettings();
            return View();
        }
    }
}