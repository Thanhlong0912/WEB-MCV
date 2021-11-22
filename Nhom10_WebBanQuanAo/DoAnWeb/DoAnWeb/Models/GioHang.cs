using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class GioHang
    {
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();
        public string sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double dGiaBan { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get { return iSoLuong * dGiaBan; } }

        public GioHang(string MaSP, int soLuong)
        {
            sMaSP = MaSP;
            SANPHAM sp = db.SANPHAMs.Single(s => s.MaSP == MaSP);
            sTenSP = sp.TenSP;
            sHinhAnh = sp.HinhAnh;
            dGiaBan = double.Parse(sp.GiaBan.ToString());
            iSoLuong = soLuong;
        }
    }
}