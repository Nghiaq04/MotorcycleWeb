using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MotorcycleWeb.Models.EF.Enums;

namespace MotorcycleWeb.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string Address { get; set; }
        public string Email { get; set; }
        public long TotalAmount { get; set; }
        public int TypePayment { get; set; }
        public OrderStatus Status { get; set; }

    }
}