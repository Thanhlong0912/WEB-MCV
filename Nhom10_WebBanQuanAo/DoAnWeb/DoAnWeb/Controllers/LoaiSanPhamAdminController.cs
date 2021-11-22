using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class LoaiSanPhamAdminController : Controller
    {
        //
        // GET: /LoaiSanPhamAdmin/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them(LOAISANPHAM lsp)
        {
            try
            {
                db.LOAISANPHAMs.InsertOnSubmit(lsp);
                db.SubmitChanges();
                return RedirectToAction("QLLoaiSanPham");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoa(string maLoai)
        {
            var lsp = db.LOAISANPHAMs.Single(i=>i.MaLoai == maLoai);
            return View(lsp);
        }

        [HttpPost]
        public ActionResult Xoa(LOAISANPHAM lsp)
        {
            try
            {
                var Loai = db.LOAISANPHAMs.SingleOrDefault(i => i.MaLoai == lsp.MaLoai);
                db.LOAISANPHAMs.DeleteOnSubmit(Loai);
                db.SubmitChanges();
                return RedirectToAction("QLLoaiSanPham");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Sua(string maLoai)
        {
            var lsp = db.LOAISANPHAMs.Single(i => i.MaLoai == maLoai);
            return View(lsp);
        }

        [HttpPost]
        public ActionResult Sua(string maLoai, LOAISANPHAM lsp)
        {
            try
            {
                var Loai = (from loaisp in db.LOAISANPHAMs
                          where loaisp.MaLoai == maLoai
                          select loaisp).SingleOrDefault();
                Loai.TenLoaiSP = lsp.TenLoaiSP;
                db.SubmitChanges();
                return RedirectToAction("QLLoaiSanPham");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult QLLoaiSanPham()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("HomeAdmin","Admin");
            }
            List<LOAISANPHAM> lstLoaiSP = db.LOAISANPHAMs.ToList();
            return View(lstLoaiSP);
        }

    }
}
