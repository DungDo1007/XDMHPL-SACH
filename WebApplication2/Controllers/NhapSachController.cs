using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class NhapSachController : Controller
    {
        THUEntities db = new THUEntities();
        // GET: NhapSach
        public ActionResult NhapSach()
        {
            ViewBag.nxblist = db.NXBs.ToList();
            return View();
        }
        public JsonResult getsach(int nxbid)
        {
            return Json(db.Saches.Where(x => x.fk_nxbID == nxbid).Select(x => new
            {
                Id = x.sachID,
                Name = x.sach_ten
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult save(NhapSachVM O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                NhapSachMaster order = new NhapSachMaster { fk_nxbID = O.fk_nxbID,
                    nhapsach_ngaygiao = O.nhapsach_ngaygiao,
                    nhapsach_nguoigiao = O.nhapsach_nguoigiao };
                foreach (var i in O.NhapSachDetails)
                {
                    order.NhapSachDetails.Add(i);
                }
                db.NhapSachMasters.Add(order);
                foreach (var i in O.NhapSachDetails)
                {
                    NXBDebt l = db.NXBDebts.SingleOrDefault(x => x.fk_sachID == i.fk_sachID &&
                    x.fk_nxbID == O.fk_nxbID);
                
                    if (l != null)
                    {
                        l.nxbdebt_soluong += i.soluong;
                    }
                    else
                    {
                        NXBDebt k = new NXBDebt { fk_nxbID = O.fk_nxbID, fk_sachID = i.fk_sachID,
                        nxbdebt_soluong = i.soluong, nxbdebt_banduoc = 0 };
                        db.NXBDebts.Add(k);
                    }
                }
                foreach (var i in O.NhapSachDetails)
                {
                    Sach w = db.Saches.SingleOrDefault(x => x.sachID == i.fk_sachID);
                    w.sach_soluong += i.soluong;
                }
                foreach (var i in O.NhapSachDetails)
                {
                    List<TonKho> la = db.TonKhoes.Where(x => x.fk_sachID == i.fk_sachID).ToList();
                    var maxdate1 = la.Select(x => x.tonkho_ngaycapnhat).Max();
                    TonKho u = db.TonKhoes.Where(s => s.fk_sachID == i.fk_sachID && s.tonkho_ngaycapnhat == maxdate1).FirstOrDefault();
                    if (u != null)
                    {
                        if (O.nhapsach_ngaygiao == u.tonkho_ngaycapnhat)
                        {
                            u.tonkho_soluong += i.soluong;
                        }
                        else if (O.nhapsach_ngaygiao > u.tonkho_ngaycapnhat)
                        {
                            u.tonkho_soluong += i.soluong;
                            u.fk_sachID = i.fk_sachID;
                            u.tonkho_ngaycapnhat = O.nhapsach_ngaygiao;
                            db.TonKhoes.Add(u);
                        }
                        else if (O.nhapsach_ngaygiao < u.tonkho_ngaycapnhat)
                        {
                            List<TonKho> bb = db.TonKhoes.Where(x => x.fk_sachID == i.fk_sachID && x.tonkho_ngaycapnhat >= O.nhapsach_ngaygiao).ToList();
                            foreach (var c in bb)
                            {
                                c.tonkho_soluong += i.soluong;
                            }
                            TonKho h = db.TonKhoes.Where(x => x.fk_sachID == i.fk_sachID && x.tonkho_ngaycapnhat == O.nhapsach_ngaygiao).SingleOrDefault();
                            if (h == null)
                            {
                                TonKho a = new TonKho { fk_sachID = i.fk_sachID, tonkho_soluong = i.soluong, tonkho_ngaycapnhat = O.nhapsach_ngaygiao };
                                db.TonKhoes.Add(a);
                            }           
                        }
                    }
                    else
                    {
                        TonKho p = new TonKho { fk_sachID = i.fk_sachID, tonkho_soluong = i.soluong, tonkho_ngaycapnhat = O.nhapsach_ngaygiao };
                        db.TonKhoes.Add(p);
                    }
                }
            }
            foreach (var i in O.NhapSachDetails)
            {
                List<NXBDebtTien> li = db.NXBDebtTiens.Where(x => x.fk_nxbID == O.fk_nxbID).ToList();
                var maxdate = li.Select(x => x.nxbdebttien_ngaycapnhat).Max(); 
                NXBDebtTien n = db.NXBDebtTiens.Where(s => s.fk_nxbID == O.fk_nxbID && s.nxbdebttien_ngaycapnhat == maxdate).FirstOrDefault();    
                var gn = db.Saches.Where(x => x.sachID == i.fk_sachID).Select(x=>x.sach_gianhap).SingleOrDefault();
                if (n != null)
                {
                    if (O.nhapsach_ngaygiao == n.nxbdebttien_ngaycapnhat)
                    {
                        n.nxbdebttien_tien += gn * i.soluong;
                    }
                    else if (O.nhapsach_ngaygiao > n.nxbdebttien_ngaycapnhat)
                    {
                        n.nxbdebttien_tien += gn * i.soluong;
                        n.fk_nxbID = O.fk_nxbID;
                        n.nxbdebttien_ngaycapnhat = O.nhapsach_ngaygiao;
                        db.NXBDebtTiens.Add(n);
                    }
                    else if (O.nhapsach_ngaygiao < n.nxbdebttien_ngaycapnhat)
                    {
                        
                        List<NXBDebtTien> dd = db.NXBDebtTiens.Where(x => x.fk_nxbID == O.fk_nxbID && x.nxbdebttien_ngaycapnhat >= O.nhapsach_ngaygiao).ToList();
                        foreach (var c in dd)
                        {
                            c.nxbdebttien_tien += gn * i.soluong;
                        }
                        NXBDebtTien m = db.NXBDebtTiens.Where(x => x.fk_nxbID == O.fk_nxbID && x.nxbdebttien_ngaycapnhat == O.nhapsach_ngaygiao).SingleOrDefault();
                        if (m == null)
                        {
                            NXBDebtTien v = new NXBDebtTien { fk_nxbID = O.fk_nxbID, nxbdebttien_tien = gn, nxbdebttien_ngaycapnhat = O.nhapsach_ngaygiao };
                            db.NXBDebtTiens.Add(v);
                        }
                    }
                }
                else
                {
                    NXBDebtTien q = new NXBDebtTien { fk_nxbID = O.fk_nxbID, nxbdebttien_tien = gn * i.soluong, nxbdebttien_ngaycapnhat = O.nhapsach_ngaygiao };
                    db.NXBDebtTiens.Add(q);
                }
            }
            db.SaveChanges();
            status = true;
            return new JsonResult { Data = new { status = status } };
        }
    }
}
