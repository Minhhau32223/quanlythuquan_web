using Quanlythuquanweb.Models;
using System.Linq;
using System.Web.Mvc;

public class ProfileController : Controller
{
    private AppDbContext db = new AppDbContext();

    public ActionResult Index()
    {
        var tv = db.thanhvien.ToList();
        return View(tv);
    }
}
