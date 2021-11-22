using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class User
    {
        [Required(ErrorMessage="Vui lòng nhập tên đăng nhập")]
        [StringLength(20)]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lê")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giới tính")]
        public string GioiTinh { get; set; }
    }
}