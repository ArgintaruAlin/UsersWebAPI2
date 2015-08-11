using System.Web.Mvc;

namespace WebApi2.Controllers
{
    public class UserViewController : Controller
    {
        // GET: UserView
        public ActionResult Index()
        {
            return View();
        }
    }
}