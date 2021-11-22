using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class TaiKhoanController : Controller
    {
        //
        // GET: /TaiKhoan/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(User us)
        {
            if (ModelState.IsValid)
            {
                var lst = from kh in db.KHACHHANGs
                               where kh.TenDangNhap == us.TenDangNhap
                               select kh;
                if (lst.Count() > 0)
                {
                    ViewBag.TrungTenDangNhap = "Tên đăng nhập đã có sẵn";
                    return View();
                }

                lst = from kh in db.KHACHHANGs
                          where kh.Email == us.Email
                          select kh;
                if (lst.Count() > 0)
                {
                    ViewBag.TrungEmail = "Email đã có sẵn";
                    return View();
                }
                lst = from kh in db.KHACHHANGs
                      where kh.SoDT == us.SDT
                      select kh;
                if (lst.Count() > 0)
                {
                    ViewBag.TrungSDT = "Số điện thoại đã có sẵn";
                    return View();
                }
                
                KHACHHANG KH = new KHACHHANG();
                KH.TenDangNhap = us.TenDangNhap;
                KH.MatKhau = us.MatKhau;
                KH.Hoten = us.HoTen;
                KH.Email = us.Email;
                KH.SoDT = us.SDT;
                KH.GioiTinh = us.GioiTinh;
                KH.DiaChi = us.DiaChi;
                db.KHACHHANGs.InsertOnSubmit(KH);
                db.SubmitChanges();

                Session["TaiKhoan"] = us.TenDangNhap;
                ViewBag.TrungTenDangNhap = "";
                ViewBag.TrungEmail = "";
                ViewBag.TrungSDT = "";
                return RedirectToAction("TrangChu", "TrangChu");                
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(User us)
        {
            if(us.TenDangNhap!=null && us.MatKhau != null)
            {
                KHACHHANG kh = new KHACHHANG();
                try
                {
                    kh = db.KHACHHANGs.Single(k => k.TenDangNhap == us.TenDangNhap && k.MatKhau == us.MatKhau);
                }
                catch { }
                if (kh.TenDangNhap != null)
                {
                    Session["TaiKhoan"] = us.TenDangNhap;
                    ViewBag.LoiDangNhap = "";
                    return RedirectToAction("TrangChu", "TrangChu");
                }
                else
                {
                    ViewBag.LoiDangNhap = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    return View();
                }
            }
            else
                return View();
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("TrangChu", "TrangChu");
        }

        public ActionResult TaiKhoanPartial()
        {
            return PartialView();
        }
    }
}
