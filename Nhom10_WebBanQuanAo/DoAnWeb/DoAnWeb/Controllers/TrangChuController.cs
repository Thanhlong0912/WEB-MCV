using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class TrangChuController : Controller
    {
        //
        // GET: /TrangChu/
        dbQuanLyQuanAoDataContext db = new dbQuanLyQuanAoDataContext();

        public ActionResult TrangChu()
        {
            return View();
        }

    }
}
