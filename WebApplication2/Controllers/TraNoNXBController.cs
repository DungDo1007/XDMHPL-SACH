using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class TraNoNXBController : Controller
    {
        THUEntities db = new THUEntities();
        // GET: TraNoNXB
        public ActionResult TraNo()
        {
            ViewBag.nxblist = db.NXBs.ToList();
            return View();
        }
        public JsonResult GetNoList(int nxbID)
        {
            double tongtien = 0;
           List <NXBDebtView> C = db.NXBDebts.
                 Where(x => x.fk_nxbID == nxbID && x.nxbdebt_banduoc > 0).Select(x => new NXBDebtView
                 {
                     nxbdebtID = x.nxbdebtID,
                     fk_nxbID = x.fk_nxbID,
                     fk_sachID = x.fk_sachID,
                     SACH_GIA = x.Sach.sach_gianhap,
                     SACH_NAME = x.Sach.sach_ten,
                     nxbdebt_soluong = x.nxbdebt_soluong,
                     nxbdebt_banduoc = x.nxbdebt_banduoc
                 }).ToList();
            foreach(var i in C)
            {
                tongtien += double.Parse(i.SACH_GIA.ToString()) * double.Parse(i.nxbdebt_banduoc.ToString());
            }
          
            return Json( new { m = C, t = tongtien }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult save(int nxbID, float tongtien)
        {
            DateTime date = DateTime.Now; var dt = date.Date;
            List<NXBDebt> n = db.NXBDebts.Where(x => x.fk_nxbID == nxbID).ToList();
            foreach(var i in n)
            {
                i.nxbdebt_soluong -= i.nxbdebt_banduoc;
                i.nxbdebt_banduoc = 0;
            }
            List<NXBDebtTien> li = db.NXBDebtTiens.Where(x => x.fk_nxbID == nxbID).ToList();
            var maxdate = li.Select(x => x.nxbdebttien_ngaycapnhat).Max();
            NXBDebtTien e = db.NXBDebtTiens.Where(x => x.fk_nxbID == nxbID && x.nxbdebttien_ngaycapnhat == maxdate).FirstOrDefault();
            if (n != null)
            {
                if (dt == e.nxbdebttien_ngaycapnhat)
                {
                    e.nxbdebttien_tien -= tongtien;
                }
                else if (dt > e.nxbdebttien_ngaycapnhat)
                {
                    e.nxbdebttien_tien -= tongtien;
                    e.fk_nxbID = nxbID;
                    e.nxbdebttien_ngaycapnhat = dt;
                    db.NXBDebtTiens.Add(e);
                }
                else if (dt < e.nxbdebttien_ngaycapnhat)
                {
                    List<NXBDebtTien> dd = db.NXBDebtTiens.Where(x => x.fk_nxbID == nxbID && x.nxbdebttien_ngaycapnhat >= dt).ToList();
                    foreach (var c in dd)
                    {
                        c.nxbdebttien_tien -= tongtien;
                    }
                    NXBDebtTien m = db.NXBDebtTiens.Where(x => x.fk_nxbID == nxbID && x.nxbdebttien_ngaycapnhat == dt).SingleOrDefault();
                    if (m == null)
                    {
                        NXBDebtTien v = new NXBDebtTien { fk_nxbID = nxbID, nxbdebttien_tien = tongtien, nxbdebttien_ngaycapnhat = dt };
                        db.NXBDebtTiens.Add(v);
                    }
                }
            }
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}