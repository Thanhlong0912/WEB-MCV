using DoAnWeb.Models;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class CuaHangController : Controller
    {
        //
        // GET: /CuaHang/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult CuaHang(int page = 1, int pagesize = 6)
        {

            List<SANPHAM> LstSP = db.SANPHAMs.Where(s => s.MaLoai == "Ao" || s.MaLoai == "Quan").OrderBy(g => g.GiaBan).ToList();
            if (LstSP.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào!";
            }
            return View(LstSP.ToPagedList(page, pagesize));
        }

        public ActionResult CuaHangAo(int page = 1, int pagesize = 6)
        {

            List<SANPHAM> LstSP = db.SANPHAMs.Where(s => s.MaLoai == "Ao").OrderBy(g => g.GiaBan).ToList();
            if (LstSP.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào!";
            }
            return View(LstSP.ToPagedList(page, pagesize));
        }

        public ActionResult CuaHangQuan(int page = 1, int pagesize = 6)
        {

            List<SANPHAM> LstSP = db.SANPHAMs.Where(s => s.MaLoai == "Quan").OrderBy(g => g.GiaBan).ToList();
            if (LstSP.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào!";
            }
            return View(LstSP.ToPagedList(page, pagesize));
        }
    }
}
