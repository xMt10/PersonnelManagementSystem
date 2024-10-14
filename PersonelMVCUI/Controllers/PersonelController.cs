using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    [Authorize(Roles = "A,U")]
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList(); // EAGER LOADING : Personel tablosu ile Departman tablosunu joinleyecek 
            return View(model);
        }
        
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personel()
            };
            return View("PersonelForm",model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            
            if (!ModelState.IsValid) {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonelForm", model);
            }
        


            if(personel.Id == 0)// Ekleme işlemi
               db.Personel.Add(personel);
            else // Güncelleme İşlemi
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)
            };
            return View("PersonelForm",model);

        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            db.Personel.Remove(silinecekPersonel);
            if (silinecekPersonel == null)
                HttpNotFound();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}