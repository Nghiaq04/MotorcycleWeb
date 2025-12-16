using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MotorcycleWeb.Models.EF
{
    [Table("tb_SettingSystem")]
    public class SystemSetting
    {
        [Key]
        [StringLength(50)]
        public string SettingKey { get; set; }
        [Required]
        public string SettingValue { get; set; }
        public string SettingDescription { get; set; }
    }
}