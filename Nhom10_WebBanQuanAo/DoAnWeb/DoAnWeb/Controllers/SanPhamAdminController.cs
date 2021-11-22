using DoAnWeb.Models;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DoAnWeb.Controllers
{
    public class SanPhamAdminController : Controller
    {
        //
        // GET: /SanPhamAdmin/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult SanPhamAdmin(int page = 1, int pagesize = 5)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("HomeAdmin", "Admin");
            }
            return View(db.SANPHAMs.ToList().ToPagedList(page,pagesize));
        }

        public ActionResult Them()
        {
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoaiSP");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(SANPHAM sp, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    HinhAnh.SaveAs(Path.Combine(Server.MapPath("~/Images/sanpham"), HinhAnh.FileName));
                    sp.HinhAnh = HinhAnh.FileName;
                    db.SANPHAMs.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    return RedirectToAction("SanPhamAdmin");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoa(string maSP)
        {
            return View(db.SANPHAMs.SingleOrDefault(i=>i.MaSP==maSP));
        }

        [HttpPost]
        public ActionResult Xoa(string MaSP, SANPHAM sp)
        {
            try
            {
                var SP = db.SANPHAMs.SingleOrDefault(i => i.MaSP == MaSP);
                db.SANPHAMs.DeleteOnSubmit(SP);
                db.SubmitChanges();
                return RedirectToAction("SanPhamAdmin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Sua(string maSP)
        {
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoaiSP",db.SANPHAMs.Single(i=>i.MaSP==maSP).MaLoai);
            return View(db.SANPHAMs.SingleOrDefault(i=>i.MaSP==maSP));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(SANPHAM sp, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    HinhAnh.SaveAs(Path.Combine(Server.MapPath("~/Images/sanpham"), HinhAnh.FileName));
                    sp.HinhAnh = HinhAnh.FileName;
                }
                var SP = (from sanpham in db.SANPHAMs
                            where sanpham.MaSP == sp.MaSP
                            select sanpham).SingleOrDefault();
                SP.TenSP = sp.TenSP;
                SP.GiaBan = sp.GiaBan;
                SP.GiaCu = sp.GiaCu;
                SP.MoTa = sp.MoTa;
                SP.MaLoai = sp.MaLoai;
                db.SubmitChanges();

                return RedirectToAction("SanPhamAdmin");
            }
            catch
            {
                return View();
            }
        }

    }
}
