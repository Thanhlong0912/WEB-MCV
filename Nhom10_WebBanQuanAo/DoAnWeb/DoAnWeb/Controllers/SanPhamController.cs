using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult ChiTietSanPham(string maSP)
        {
            SANPHAM SP = db.SANPHAMs.SingleOrDefault(sp => sp.MaSP == maSP);
            return View(SP);
        }

        public ActionResult Top4SanPhamMoi()
        {
            List<SANPHAM> top4 = db.SANPHAMs.Take(4).OrderByDescending(sp => sp.GiaBan).ToList();
            return PartialView(top4);
        }

        public ActionResult Top4GiaRe()
        {
            List<SANPHAM> top4 = db.SANPHAMs.Take(4).OrderBy(sp => sp.GiaBan).ToList();
            return PartialView(top4);
        }
    }
}
