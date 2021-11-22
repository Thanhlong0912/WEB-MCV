using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();
        
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(string maSP,int soLuong, string strURL)
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(sp => sp.sMaSP == maSP);
            
            if (SanPham == null)
            {
                SanPham = new GioHang(maSP,soLuong);
                lstGioHang.Add(SanPham);

                try
                {
                    //thêm sản phẩm vào giỏ hàng database
                    GIOHANG GH = new GIOHANG();
                    GH.TenDangNhapKhachHang = Session["TaiKhoan"].ToString();
                    GH.MaSP = maSP;
                    GH.SoLuongSP = soLuong;
                    db.GIOHANGs.InsertOnSubmit(GH);
                    db.SubmitChanges();
                }
                catch { }
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong+=soLuong;

                //update lại số lượng trong database
                var GH = (from gh in db.GIOHANGs
                                 where gh.MaSP == maSP
                                 select gh
                            ).SingleOrDefault();
                GH.SoLuongSP += soLuong;
                db.SubmitChanges();

                return Redirect(strURL);
            }
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "TrangChu");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }

        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.dThanhTien);
            }
            return ttt;
        }

        public ActionResult CapNhat(string maSP, int soLuong)
        {

            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(sp => sp.sMaSP == maSP);
            if (SanPham != null)
            {
                SanPham.iSoLuong = soLuong;

                //update lại số lượng trong database
                var GH = (from gh in db.GIOHANGs
                          where gh.MaSP == maSP
                          select gh
                            ).SingleOrDefault();
                GH.SoLuongSP = soLuong;
                db.SubmitChanges();
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult Xoa(string maSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.sMaSP == maSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.sMaSP == maSP);

                //xóa sp đó trong database
                var SP = db.GIOHANGs.SingleOrDefault(tv => tv.MaSP == maSP);
                db.GIOHANGs.DeleteOnSubmit(SP);
                db.SubmitChanges();

                return RedirectToAction("GioHang", "GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "TrangChu");
            }
            return RedirectToAction("GioHang", "GioHang");
        }

    }
}
