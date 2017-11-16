using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ThemSachController : Controller
    {
        // GET: DauSach

        THUEntities db = new THUEntities();
        public ActionResult ThemSach()
        {
            List<NXB> pubList = db.NXBs.ToList();
            ViewBag.NXBLIST = new SelectList(pubList, "nxbID", "nxb_ten");
            return View();
        }
        public JsonResult GetBookList()
        {
            List<BookViewModel> BookList = db.Saches.Select(x => new BookViewModel
            {
                sachID = x.sachID,
                sach_ten = x.sach_ten,
                sach_tacgia = x.sach_tacgia,
                sach_gianhap = x.sach_gianhap,
                sach_giaxuat = x.sach_giaxuat,
                sach_soluong = x.sach_soluong,
                NXB_NAME = x.NXB.nxb_ten,
            }).ToList();

            return Json(BookList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBookById(int bookId)
        {
            Sach model = db.Saches.Where(x => x.sachID == bookId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveDataInDatabase(BookViewModel model)
        {
            var result = false;
            try
            {
                if (model.sachID > 0)
                {
                    Sach b = db.Saches.SingleOrDefault(x => x.sachID == model.sachID);
                    b.sach_ten = model.sach_ten;
                    b.sach_tacgia = model.sach_tacgia;
                    b.sach_gianhap = model.sach_gianhap;
                    b.sach_giaxuat = model.sach_giaxuat;
                    b.fk_nxbID = model.fk_nxbID;
                    db.SaveChanges(); result = true;
                }
                else
                {
                    Sach b = new Sach();
                    b.sach_ten = model.sach_ten;
                    b.sach_tacgia = model.sach_tacgia;
                    b.sach_gianhap = model.sach_gianhap;
                    b.sach_giaxuat = model.sach_giaxuat;
                    b.fk_nxbID = model.fk_nxbID;
                    b.sach_soluong = 0;
                    db.Saches.Add(b);
                    db.SaveChanges(); result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteBookRecord(int bookId)
        {
            bool result = false;
            Sach b = db.Saches.SingleOrDefault(x => x.sachID == bookId);
            if (b != null)
            {
                db.Saches.Remove(b);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}