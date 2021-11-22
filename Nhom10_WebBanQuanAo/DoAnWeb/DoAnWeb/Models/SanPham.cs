using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class SanPham
    {
        private SqlConnection con = new SqlConnection("Data Source = DESKTOP-QAP7SKJ; Database = QuanLy_QuanAo;User Id=sa;Password=123;");
        
        public List<SANPHAM> Search(string txt_Search)
        {
            List<SANPHAM> listSP = new List<SANPHAM>();

            string sql = "Select * from dbo.SANPHAM where TenSP LIKE '%' + @tenSP +'%'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            SqlParameter par = new SqlParameter("@tenSP", txt_Search);
            cmd.Parameters.Add(par);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                SANPHAM sp = new SANPHAM();
                sp.MaSP = rdr.GetValue(0).ToString();
                sp.TenSP = rdr.GetValue(1).ToString();
                sp.GiaBan = int.Parse(rdr.GetValue(2).ToString());
                sp.HinhAnh = rdr.GetValue(5).ToString();
                listSP.Add(sp);
            }

            return listSP;
        }
    }
}