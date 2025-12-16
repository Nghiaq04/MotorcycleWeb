using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorcycleWeb.Models.EF
{
    public class Enums
    {
        public enum OrderStatus
        {
            Confirmed = 1,    // Xác nhận
            Delivering = 2,   // Đang giao
            Delivered = 3     // Giao thành công
        }
    }
}