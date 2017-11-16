using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class XuatSachController : Controller
    {
        THUEntities db = new THUEntities();
        // GET: NhapSach
        public ActionResult XuatSach()
        {
            ViewBag.dailylist = db.DaiLies.ToList();
            ViewBag.nxblist = db.NXBs.ToList();
            return View();
        }
        private float TinhNoTon(int dl)
        {
            float no;
            List<DaiLyDebtTien> lu = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == dl).ToList();
            var maxdate = lu.Select(x => x.dailydebttien_ngaycapnhat).Max();
            DaiLyDebtTien n = db.DaiLyDebtTiens.Where(s => s.fk_dailyID == dl && s.dailydebttien_ngaycapnhat == maxdate).FirstOrDefault();
            if (n != null)
            {
                no = float.Parse(n.dailydebttien_tien.ToString());
            }
            else
            {
                no = 0;
            }
            return no;
        }
        public ActionResult LapPhieuXuat(int orderID)
        {
            var dl = db.XuatSachMasters.Where(x => x.xuatsachID == orderID).Select(x => x.fk_dailyID).SingleOrDefault();
            DaiLy n = db.DaiLies.Where(x => x.dailyID == dl).SingleOrDefault();
            Session["dailyID"] = n.dailyID;
            Session["dailyten"] = n.daily_ten;
            ViewBag.dailyID = n.dailyID;
            ViewBag.DebtMax = n.daily_debtmax;
            ViewBag.DebtTon = TinhNoTon(n.dailyID);
            ViewBag.orderID = orderID;
            ViewBag.TONGTIEN = db.XuatSachMasters.Where(x => x.xuatsachID == orderID).Select(x => x.xuatsach_tongtien).SingleOrDefault().ToString();
            return View();
        }
        public JsonResult GetPhieuXuatList()
        {
            List<XuatSachMasterView> BookList = db.XuatSachMasters.Where(x => x.xuatsach_trangthai == "Đã Đặt Sách")
                .Select(x => new XuatSachMasterView
                {
                    xuatsachID = x.xuatsachID,
                    xuatsach_ngayorder = x.xuatsach_ngayorder,
                    xuatsach_ngayupdate = x.xuatsach_ngayupdate,
                    xuatsach_nguoinhan = x.xuatsach_nguoinhan,
                    xuatsach_trangthai = x.xuatsach_trangthai,
                    xuatsach_tongtien = x.xuatsach_tongtien,
                    DAILY_NAME = x.DaiLy.daily_ten
                }).ToList();
            return Json(BookList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChiTietList(int orderId)
        {
            List<XuatSachDetailsView> BookList = db.XuatSachDetails.Where(x => x.xuatsachID == orderId)
                .Select(x => new XuatSachDetailsView
                {
                    ctxuatsachID = x.ctxuatsachID,
                    xuatsachID = x.xuatsachID,
                    fk_sachID = x.fk_sachID,
                    soluong = x.soluong,
                    soluongton = x.Sach.sach_soluong,
                    SACH_GIA = x.Sach.sach_giaxuat,
                    SACH_NAME = x.Sach.sach_ten,
                }).ToList();

            return Json(BookList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult save(XuatSachMaster O)
        {
            int dlyID = int.Parse(Session["dailyID"].ToString());
            bool status = false;
            if (ModelState.IsValid)
            {
                XuatSachMaster order = db.XuatSachMasters.Where(x => x.xuatsachID == O.xuatsachID).SingleOrDefault();
                order.xuatsach_ngayupdate = O.xuatsach_ngayupdate;
                order.xuatsach_nguoinhan = O.xuatsach_nguoinhan;
                order.xuatsach_trangthai = "Hoàn Tất";
                List<XuatSachDetail> dt = db.XuatSachDetails.Where(x => x.xuatsachID == O.xuatsachID).ToList();
                int sumsl = 0;
                foreach (var i in dt)
                {
                    sumsl += int.Parse(i.soluong.ToString());
                }
                foreach (var i in dt)
                {
                    DaiLyDebt l = db.DaiLyDebts.SingleOrDefault(x => x.fk_sachID == i.fk_sachID && x.fk_dailyID == dlyID);
                    if (l != null)
                    {
                        l.dailydebt_soluong += i.soluong;
                    }
                    else
                    {
                        DaiLyDebt k = new DaiLyDebt { fk_dailyID = dlyID, fk_sachID = i.fk_sachID, dailydebt_soluong = i.soluong };
                        db.DaiLyDebts.Add(k);
                    }
                }
                foreach (var i in dt)
                {
                    Sach w = db.Saches.SingleOrDefault(x => x.sachID == i.fk_sachID);
                    w.sach_soluong -= i.soluong;
                }
                List<DaiLyDebtTien> li = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == dlyID).ToList();
                var maxdate = li.Select(x => x.dailydebttien_ngaycapnhat).Max();
                DaiLyDebtTien n = db.DaiLyDebtTiens.Where(s => s.fk_dailyID == dlyID && s.dailydebttien_ngaycapnhat == maxdate).FirstOrDefault();
                if (n != null)
                {
                    if (O.xuatsach_ngayupdate == n.dailydebttien_ngaycapnhat)
                    {
                        n.dailydebttien_tien += O.xuatsach_tongtien;
                        n.dailydebttien_sach += sumsl;
                    }
                    else if (O.xuatsach_ngayupdate > n.dailydebttien_ngaycapnhat)
                    {
                        n.dailydebttien_tien += O.xuatsach_tongtien;
                        n.dailydebttien_sach += sumsl;
                        n.fk_dailyID = dlyID;
                        n.dailydebttien_ngaycapnhat = O.xuatsach_ngayupdate;
                        db.DaiLyDebtTiens.Add(n);
                    }
                    else if (O.xuatsach_ngayupdate < n.dailydebttien_ngaycapnhat)
                    {
                        List<DaiLyDebtTien> dd = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == dlyID && x.dailydebttien_ngaycapnhat >= O.xuatsach_ngayupdate).ToList();
                        foreach (var c in dd)
                        {
                            c.dailydebttien_tien += O.xuatsach_tongtien;
                            c.dailydebttien_sach += sumsl;
                        }
                        DaiLyDebtTien m = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == dlyID && x.dailydebttien_ngaycapnhat == O.xuatsach_ngayupdate).SingleOrDefault();
                        if (m == null)
                        {
                            DaiLyDebtTien v = new DaiLyDebtTien { fk_dailyID = dlyID, dailydebttien_tien = O.xuatsach_tongtien, dailydebttien_sach = sumsl, dailydebttien_ngaycapnhat = O.xuatsach_ngayupdate };
                            db.DaiLyDebtTiens.Add(v);
                        }
                    }
                }
                else
                {
                    DaiLyDebtTien q = new DaiLyDebtTien { fk_dailyID = dlyID, dailydebttien_tien = O.xuatsach_tongtien, dailydebttien_sach= sumsl, dailydebttien_ngaycapnhat = O.xuatsach_ngayupdate };
                    db.DaiLyDebtTiens.Add(q);
                }

                foreach (var i in dt)
                {
                    List<TonKho> la = db.TonKhoes.Where(x => x.fk_sachID == i.fk_sachID).ToList();
                    var maxdate1 = la.Select(x => x.tonkho_ngaycapnhat).Max();
                    TonKho u = db.TonKhoes.Where(s => s.fk_sachID == i.fk_sachID && s.tonkho_ngaycapnhat == maxdate1).FirstOrDefault();
                    if (u != null)
                    {
                        if (O.xuatsach_ngayupdate == u.tonkho_ngaycapnhat)
                        {
                            u.tonkho_soluong += i.soluong;
                        }
                        else if (O.xuatsach_ngayupdate > u.tonkho_ngaycapnhat)
                        {
                            u.tonkho_soluong += i.soluong;
                            u.fk_sachID = i.fk_sachID;
                            u.tonkho_ngaycapnhat = O.xuatsach_ngayupdate;
                            db.TonKhoes.Add(u);
                        }
                        else if (O.xuatsach_ngayupdate < u.tonkho_ngaycapnhat)
                        {
                            List<TonKho> bb = db.TonKhoes.Where(x => x.fk_sachID == i.fk_sachID && x.tonkho_ngaycapnhat >= O.xuatsach_ngayupdate).ToList();
                            foreach (var c in bb)
                            {
                                c.tonkho_soluong += i.soluong;
                            }
                            TonKho h = db.TonKhoes.Where(x => x.fk_sachID == i.fk_sachID && x.tonkho_ngaycapnhat == O.xuatsach_ngayupdate).SingleOrDefault();
                            if (h == null)
                            {
                                TonKho a = new TonKho { fk_sachID = i.fk_sachID, tonkho_soluong = i.soluong, tonkho_ngaycapnhat = O.xuatsach_ngayupdate };
                                db.TonKhoes.Add(a);
                            }
                        }
                    }
                    else
                    {
                        TonKho p = new TonKho { fk_sachID = i.fk_sachID, tonkho_soluong = i.soluong, tonkho_ngaycapnhat = O.xuatsach_ngayupdate };
                        db.TonKhoes.Add(p);
                    }
                }
            }
            db.SaveChanges();
            status = true;
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult huyphieu(XuatSachMaster O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                XuatSachMaster order = db.XuatSachMasters.Where(x => x.xuatsachID == O.xuatsachID).SingleOrDefault();
                order.xuatsach_ngayupdate = O.xuatsach_ngayupdate;
                order.xuatsach_trangthai = "Hủy";
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}

