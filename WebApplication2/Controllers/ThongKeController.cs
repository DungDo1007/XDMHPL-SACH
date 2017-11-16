using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ThongKeController : Controller
    {
        THUEntities db = new THUEntities();
        // GET: ThongKe
        public ActionResult CongNo()
        {
            ViewBag.dailylist = db.DaiLies.ToList();
            return View();
        }
        public JsonResult TinhCongNo(DateTime datethis)
        {
            List<CongNoTienList> cnlist = new List<CongNoTienList>();
            List<DaiLyDebtTien> dt = db.DaiLyDebtTiens.OrderByDescending(x => x.dailydebttien_ngaycapnhat).ToList();
            List<DaiLy> dl = db.DaiLies.ToList();
            foreach (var i in dl)
            {
                List<DaiLyDebtTien> l = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == i.dailyID && x.dailydebttien_ngaycapnhat <= datethis).ToList();
                var Maxdate = l.Select(x => x.dailydebttien_ngaycapnhat).Max();
                DaiLyDebtTien b = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == i.dailyID && x.dailydebttien_ngaycapnhat == Maxdate).SingleOrDefault();
                if (b != null)
                {
                    CongNoTienList h = new CongNoTienList { fk_dailyID = i.dailyID, DAILY_NAME = i.daily_ten, congnotien = b.dailydebttien_tien, congnosach = b.dailydebttien_sach };
                    cnlist.Add(h);
                }
                else
                {
                    CongNoTienList h = new CongNoTienList { fk_dailyID = i.dailyID, DAILY_NAME = i.daily_ten, congnotien = 0, congnosach = 0 };
                    cnlist.Add(h);
                }
            }
            return Json(cnlist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TonKho()
        {
            ViewBag.dailylist = db.DaiLies.ToList();
            return View();
        }
        public JsonResult TinhTonKho(DateTime datethis)
        {
            List<TonKhoList> cnlist = new List<TonKhoList>();
            List<TonKho> dt = db.TonKhoes.ToList();
            List<Sach> dl = db.Saches.ToList();
            foreach (var i in dl)
            {
                List<TonKho> l = db.TonKhoes.Where(x => x.fk_sachID == i.sachID && x.tonkho_ngaycapnhat <= datethis).ToList();
                var Maxdate = l.Select(x => x.tonkho_ngaycapnhat).Max();
                TonKho b = db.TonKhoes.Where(x => x.fk_sachID == i.sachID && x.tonkho_ngaycapnhat == Maxdate).SingleOrDefault();
                if (b != null)
                {
                    TonKhoList h = new TonKhoList { fk_sachID = i.sachID, SACH_NAME = i.sach_ten, tonkho_soluong = b.tonkho_soluong };
                    cnlist.Add(h);
                }
                else
                {
                    TonKhoList h = new TonKhoList { fk_sachID = i.sachID, SACH_NAME = i.sach_ten, tonkho_soluong = 0 };
                    cnlist.Add(h);
                }
            }
            return Json(cnlist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NoNXB()
        {
            ViewBag.dailylist = db.DaiLies.ToList();
            return View();
        }
        public JsonResult TinhNoNXB(DateTime datethis)
        {
            List<NoNXBList> cnlist = new List<NoNXBList>();
            List<NXBDebtTien> dt = db.NXBDebtTiens.ToList();
            List<NXB> dl = db.NXBs.ToList();
            foreach (var i in dl)
            {
                List<NXBDebtTien> l = db.NXBDebtTiens.Where(x => x.fk_nxbID == i.nxbID && x.nxbdebttien_ngaycapnhat <= datethis).ToList();
                var Maxdate = l.Select(x => x.nxbdebttien_ngaycapnhat).Max();
                NXBDebtTien b = db.NXBDebtTiens.Where(x => x.fk_nxbID == i.nxbID && x.nxbdebttien_ngaycapnhat == Maxdate).SingleOrDefault();
                if (b != null)
                {
                    NoNXBList h = new NoNXBList { fk_nxbID = i.nxbID, NXB_NAME = i.nxb_ten, notiennxb = b.nxbdebttien_tien };
                    cnlist.Add(h);
                }
                else
                {
                    NoNXBList h = new NoNXBList { fk_nxbID = i.nxbID, NXB_NAME = i.nxb_ten, notiennxb = 0 };
                    cnlist.Add(h);
                }
            }
            return Json(cnlist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DoanhThu()
        {
            return View();
        }
        public JsonResult TinhDoanhThu(int year, int month)
        {
            float w = 0;
            int day = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                day = 31;
            }
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                day = 30;
            }
            if (month == 2)
            {
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { day = 29; }
                else { day = 28; }
            }
            DateTime y = new DateTime(year, month, day);
            var datethis = y.Date;
            List<DoanhThu> l = db.DoanhThus.Where(x => x.doanhthu_ngaycapnhat <= datethis).ToList();
            var Maxdate3 = l.Select(x => x.doanhthu_ngaycapnhat).Max();
            DoanhThu b = db.DoanhThus.Where(x => x.doanhthu_ngaycapnhat == Maxdate3).SingleOrDefault();
            if (b != null) { w = float.Parse(b.doanhthu_tien.ToString()); }
            else { w = 0; }
            return Json(w, JsonRequestBehavior.AllowGet);
        }
    }
}