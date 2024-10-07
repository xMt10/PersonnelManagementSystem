using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Departman
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm");
        }

        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if(departman.Id == 0)
            {
                db.Departman.Add(departman);
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman == null)
                    return HttpNotFound();
                guncellenecekDepartman.Ad = departman.Ad;    
            }
            
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");
        }


        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekDepertman = db.Departman.Find(id);
            if(silinecekDepertman == null)
                return HttpNotFound();
            db.Departman.Remove(silinecekDepertman);
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");

        }

    }
}