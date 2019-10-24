using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shipping.ViewModels.Sms
{
    public class ConfigurationSettings
    {

        [Key]
        [ScaffoldColumn(false)]
        public int RecordID { get; set; }

        [Required(ErrorMessage = "API_KEY is required")]
        [Display(Name = "Enter API_KEY Name")]
        public string API_KEY { get; set; }
        [Required(ErrorMessage = "API_SECRET is required")]
        public string API_SECRET { get; set; }
        [Required(ErrorMessage = "To_Number  is required")]
        [Display(Name = "To_Number")]
        public double To_Number { get; set; }
        //[DataType(DataType.PhoneNumber)]    
        [Required(ErrorMessage = "Message  is required")]
        public string TextMessage { get; set; }

    }
}