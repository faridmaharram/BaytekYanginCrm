using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crm_Project.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi yazınız..!")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}