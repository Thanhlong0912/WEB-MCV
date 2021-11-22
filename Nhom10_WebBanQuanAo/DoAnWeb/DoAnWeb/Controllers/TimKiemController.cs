using DoAnWeb.Models;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class TimKiemController : Controller
    {
        //
        // GET: /TimKiem/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult KetQuaTimKiem(string timkiem)
        {
            var lstSP = (from sp in db.SANPHAMs
                        where sp.TenSP.Contains(timkiem)
                        select sp);
            ViewBag.KhongTimThaySP = "";
            if (lstSP.Count() == 0)
                ViewBag.KhongTimThaySP = "Không tìm thấy sản phẩm";
            return View(lstSP.ToList());
        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page, string timkiem)
        {
            ViewBag.Key = timkiem;

            int pageNumber = (page ?? 1);
            int pageSize = 3;

            var lstSP = (from sp in db.SANPHAMs
                         where sp.TenSP.Contains(timkiem)
                         select sp).ToPagedList(pageNumber,pageSize);
            ViewBag.KhongTimThaySP = "";

            if (lstSP.Count() == 0 || timkiem == null || timkiem == "")
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(lstSP);
            }
            
            return View(lstSP);
        }

        [HttpPost]
        public ActionResult KetQuaTimKiem(int? page, FormCollection f)
        {
            string timkiem = f["timkiem"].ToString();

            ViewBag.Key = timkiem;

            int pageNumber = (page ?? 1);
            int pageSize = 3;

            var lstSP = (from sp in db.SANPHAMs
                         where sp.TenSP.Contains(timkiem)
                         select sp).ToPagedList(pageNumber, pageSize);
            ViewBag.KhongTimThaySP = "";

            if (lstSP.Count() == 0 || timkiem == null || timkiem == "")
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(lstSP);
            }

            return View(lstSP);
        }


    }
}
