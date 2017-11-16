using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DaiLyController : Controller
    {
        THUEntities db = new THUEntities();
        // GET: DaiLy
        public ActionResult DangNhap()
        {
            return View();
        }
        private string GetMD5(string txt)
        {
            string str = "";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string username = f["txtTaiKhoan"].ToString();
            string passwordMD5 = GetMD5(f["txtMatKhau"].ToString());
            DaiLy dl = db.DaiLies.SingleOrDefault(x => x.daily_user == username && x.daily_pass == passwordMD5);
            if (dl != null)
            {
                Session["DaiLyTen"] = dl.daily_ten.ToString();
                Session["DaiLyId"] = dl.dailyID;
                return Redirect("DatSach");
            }
            return View();
        }
        public ActionResult DatSach()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DatSach(int orderID)
        {
            bool result;
            XuatSachMaster k = db.XuatSachMasters.Where(x => x.xuatsachID == orderID
            && x.xuatsach_trangthai == "Chưa Đặt Sách").SingleOrDefault();
            if (k != null)
            {
                result = true;
            }
            else { result = false; }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBookList()
        {
            int k = int.Parse(Session["DaiLyId"].ToString());
            return Json(db.XuatSachMasters.Where(x => x.fk_dailyID == k)
                .OrderByDescending(x => x.xuatsachID).Select(x => new
                {
                    xuatsachID = x.xuatsachID,
                    ngayorder = x.xuatsach_ngayorder,
                    ngayupdate = x.xuatsach_ngayupdate,
                    nguoinhan = x.xuatsach_nguoinhan,
                    tongtien = x.xuatsach_tongtien,
                    trangthai = x.xuatsach_trangthai
                }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult TaoMoi()
        {
            XuatSachMaster xs = new XuatSachMaster
            {
                xuatsach_ngayorder = DateTime.Now,
                fk_dailyID = int.Parse(Session["DaiLyId"].ToString()),
                xuatsach_ngayupdate = new DateTime(2000, 01, 01),
                xuatsach_nguoinhan = "",
                xuatsach_tongtien = 0,
                xuatsach_trangthai = "Chưa Đặt Sách"
            };
            db.XuatSachMasters.Add(xs); db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuaChiTiet(int orderId)
        {
            ViewBag.orderId = orderId.ToString();
            ViewBag.sachlist = db.Saches.ToList();
            return View();
        }
        public ActionResult BaoSach()
        {
            int i = int.Parse(Session["DaiLyId"].ToString());
            List<DaiLyDebtView> BookList = db.DaiLyDebts.
                Where(x=>x.fk_dailyID == i).Select(x => new DaiLyDebtView
            {
                dailydebtID = x.dailydebtID,
                fk_dailyID = x.fk_dailyID,
                fk_sachID = x.fk_sachID,
                dailydebt_soluong = x.dailydebt_soluong,
                SACH_NAME = x.Sach.sach_ten
            }).ToList();
            ViewBag.DaiLyDebtlist = BookList;
            return View();
        }
        public JsonResult getdebt(int sachid)
        {
            int i = int.Parse(Session["DaiLyId"].ToString());
            DaiLyDebt d = db.DaiLyDebts.Where(x => x.fk_dailyID == i && x.fk_sachID == sachid).SingleOrDefault();
            int sldebt = int.Parse(d.dailydebt_soluong.ToString());
            Sach s = db.Saches.Where(x => x.sachID == sachid).SingleOrDefault();
            double gia = double.Parse(s.sach_giaxuat.ToString());
            return Json(new { sldebt = sldebt, gia = gia }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult XemChiTiet(int orderId)
        {
            ViewBag.orderId = orderId.ToString();
            return View();
        }
        public JsonResult GetChiTietList(int orderId)
        {
            List<XuatSachDetailsView> BookList = db.XuatSachDetails.Where(x=>x.xuatsachID == orderId)
                .Select(x => new XuatSachDetailsView
            {
                ctxuatsachID  = x.ctxuatsachID,
                xuatsachID = x.xuatsachID,
                fk_sachID = x.fk_sachID,
                soluong = x.soluong,
                SACH_GIA = x.Sach.sach_giaxuat,
                SACH_NAME = x.Sach.sach_ten,
            }).ToList();
            return Json(BookList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDebtList()
        {
            int i = int.Parse(Session["DaiLyId"].ToString());
            List<DaiLyDebtView> m = db.DaiLyDebts.
                 Where(x => x.fk_dailyID == i).Select(x => new DaiLyDebtView
                 {
                     dailydebtID = x.dailydebtID,
                     fk_dailyID = x.fk_dailyID,
                     fk_sachID = x.fk_sachID,
                     SACH_GIA = x.Sach.sach_giaxuat,
                     dailydebt_soluong = x.dailydebt_soluong,
                     SACH_NAME = x.Sach.sach_ten
                 }).ToList();
            return Json(m, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getsach(int nxbid)
        {
            return Json(db.Saches.Where(x => x.fk_nxbID == nxbid).Select(x => new
            {
                Id = x.sachID,
                Name = x.sach_ten
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getgia(int sachid)
        {
            double gia = 0;
            Sach s = db.Saches.Where(x => x.sachID == sachid).FirstOrDefault();
            if (s != null)
            {
                gia = double.Parse(s.sach_giaxuat.ToString());
            }
            return Json(new { gia = gia }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult save(XuatSachDetail O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                XuatSachDetail u = db.XuatSachDetails.Where(x => x.xuatsachID == O.xuatsachID
                    && x.fk_sachID == O.fk_sachID).SingleOrDefault();
                if(u != null) { u.soluong += O.soluong; }
                else
                {
                    XuatSachDetail order = new XuatSachDetail { xuatsachID = O.xuatsachID, fk_sachID = O.fk_sachID, soluong = O.soluong };
                    db.XuatSachDetails.Add(order);
                }
                XuatSachMaster m = db.XuatSachMasters.Where(x => x.xuatsachID == O.xuatsachID).SingleOrDefault();
                Sach s = db.Saches.Where(x => x.sachID == O.fk_sachID).SingleOrDefault();
                m.xuatsach_tongtien += O.soluong * s.sach_giaxuat;
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
        public JsonResult saveBaoSach(DaiLyDebt O)
        {
            DateTime date = DateTime.Now; var dt = date.Date;
            bool status = false;
            if (ModelState.IsValid)
            {
                int i = int.Parse(Session["DaiLyId"].ToString());
                NXBDebt u = db.NXBDebts.Where(x => x.fk_sachID == O.fk_sachID).SingleOrDefault();
                if(u!=null)
                {
                    u.nxbdebt_banduoc += O.dailydebt_soluong;
                }
                
                DaiLyDebt d = db.DaiLyDebts.Where(x => x.fk_dailyID == i && x.fk_sachID == O.fk_sachID).SingleOrDefault();
                d.dailydebt_soluong -= O.dailydebt_soluong;
                if(d.dailydebt_soluong == 0)    { db.DaiLyDebts.Remove(d); } 
                List<DaiLyDebtTien> li = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == i).ToList();
                var maxdate = li.Select(x => x.dailydebttien_ngaycapnhat).Max();
                DaiLyDebtTien n = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == i && x.dailydebttien_ngaycapnhat == maxdate).FirstOrDefault();
                Sach s = db.Saches.Where(x => x.sachID == O.fk_sachID).SingleOrDefault();
                var maxDdoanhthu = db.DoanhThus.Select(x => x.doanhthu_ngaycapnhat).Max();
                DoanhThu r = db.DoanhThus.Where(x => x.doanhthu_ngaycapnhat == maxDdoanhthu).SingleOrDefault();
                if(r != null)
                {
                    if (dt == r.doanhthu_ngaycapnhat)
                    {
                        r.doanhthu_tien += O.dailydebt_soluong * s.sach_giaxuat;
                    }
                    else if (dt > r.doanhthu_ngaycapnhat)
                    {
                        r.doanhthu_tien += O.dailydebt_soluong * s.sach_giaxuat;
                        r.doanhthu_ngaycapnhat = dt;
                        db.DoanhThus.Add(r);
                    }
                    else if (dt < r.doanhthu_ngaycapnhat)
                    {
                        List<DoanhThu> dd = db.DoanhThus.Where(x =>x.doanhthu_ngaycapnhat >= dt).ToList();
                        foreach (var c in dd)
                        {
                            r.doanhthu_tien += O.dailydebt_soluong * s.sach_giaxuat;
                        }
                        DoanhThu m = db.DoanhThus.Where(x => x.doanhthu_ngaycapnhat == dt).SingleOrDefault();
                        if (m == null)
                        {
                            DoanhThu v = new DoanhThu { doanhthu_tien = O.dailydebt_soluong * s.sach_giaxuat, doanhthu_ngaycapnhat = dt };
                            db.DoanhThus.Add(v);
                        }
                    }
                }
                else
                {
                    DoanhThu v = new DoanhThu { doanhthu_tien = O.dailydebt_soluong * s.sach_giaxuat, doanhthu_ngaycapnhat = dt };
                    db.DoanhThus.Add(v);
                }
                if (n != null)
                {
                    if (dt == n.dailydebttien_ngaycapnhat)
                    {
                        n.dailydebttien_tien -= O.dailydebt_soluong * s.sach_giaxuat;
                        n.dailydebttien_sach -= O.dailydebt_soluong;
                    }
                    else if (dt > n.dailydebttien_ngaycapnhat)
                    {
                        n.dailydebttien_tien -= O.dailydebt_soluong * s.sach_giaxuat;
                        n.dailydebttien_sach -= O.dailydebt_soluong;
                        n.fk_dailyID = i;
                        n.dailydebttien_ngaycapnhat = dt;
                        db.DaiLyDebtTiens.Add(n);
                    }
                    else if (dt < n.dailydebttien_ngaycapnhat)
                    {
                        List<DaiLyDebtTien> dd = db.DaiLyDebtTiens.Where(x => x.fk_dailyID == i && x.dailydebttien_ngaycapnhat >= dt).ToList();
                        foreach (var c in dd)
                        {
                            c.dailydebttien_tien -= O.dailydebt_soluong * s.sach_giaxuat;
                            c.dailydebttien_sach -= O.dailydebt_soluong;
                        }
                    }
                  
                    db.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }

            }
            return new JsonResult { Data = new { status = status } };
        }
        public JsonResult DeleteDetail(int detailId)
        {
            bool result = false;
            XuatSachDetail b = db.XuatSachDetails.SingleOrDefault(x => x.ctxuatsachID == detailId);
            if (b != null)
            {
                db.XuatSachDetails.Remove(b);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DLDatsach(int orderId)
        {
            bool result = false;
            XuatSachMaster h = db.XuatSachMasters.Where(x => x.xuatsachID == orderId).SingleOrDefault();
            if(h!=null)
            {
                if (h.xuatsach_tongtien > 0)
                {
                    h.xuatsach_trangthai = "Đã Đặt Sách"; db.SaveChanges(); result = true;
                }         
            }      
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}