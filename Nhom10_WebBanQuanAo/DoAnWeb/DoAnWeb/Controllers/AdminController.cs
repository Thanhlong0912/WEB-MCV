using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        [HttpGet]
        public ActionResult HomeAdmin()
        {
            Session["Admin"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult HomeAdmin(User us)
        {
            if (us.TenDangNhap != null && us.MatKhau != null)
            {
                QUANTRIVIEN qtv = new QUANTRIVIEN();
                try
                {
                    qtv = db.QUANTRIVIENs.Single(k => k.TenDangNhap == us.TenDangNhap && k.MatKhau == us.MatKhau);
                }
                catch { }
                if (qtv.TenDangNhap != null)
                {
                    Session["Admin"] = us.TenDangNhap;
                    ViewBag.LoiDangNhap = "";
                    return RedirectToAction("QLLoaiSanPham", "LoaiSanPhamAdmin");
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
            Session["Admin"] = null;
            return RedirectToAction("HomeAdmin", "Admin");
        }

        public ActionResult TaiKhoanAdminPartial()
        {
            return PartialView();
        }
    }
}
