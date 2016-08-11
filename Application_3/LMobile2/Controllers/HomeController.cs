using System.Web.Mvc;

namespace LMobile2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult Save()
        {
            return PartialView("_ServiceAuftrageList");
        }
        
        public PartialViewResult Edit(string auftragNummer)
        {            
            if (!string.IsNullOrEmpty(auftragNummer))
            {
                return PartialView("_ServiceAuftrag", auftragNummer);
            }
            else
            {
                return PartialView("_ServiceAuftrageList");
            }
        }

        public PartialViewResult Cancel()
        {
            return PartialView("_ServiceAuftrageList");
        }
        
        public PartialViewResult AddServiceAuftrag()
        {
            return PartialView("_ServiceAuftrag");
        }
    }
}