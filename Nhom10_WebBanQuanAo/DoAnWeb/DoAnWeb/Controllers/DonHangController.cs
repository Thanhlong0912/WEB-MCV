using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class DonHangController : Controller
    {
        //
        // GET: /DonHang/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult DonHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                List<DONHANG> lstDonHang = new List<DONHANG>();
                foreach (GioHang item in lstGioHang)
                {
                    DONHANG DH = new DONHANG();
                    DH.TenDangNhapKhachHang = Session["TaiKhoan"].ToString();
                    DH.MaSP = item.sMaSP;
                    DH.SoLuong = item.iSoLuong;
                    DH.TongTien = item.dThanhTien;
                    DH.NgayDatHang = DateTime.Now;
                    DH.TrangThai = "Chưa Giao";
                    db.DONHANGs.InsertOnSubmit(DH);
                    db.SubmitChanges();
                }
                Session["GioHang"] = null;
            }
            return View();
        }

    }
}
