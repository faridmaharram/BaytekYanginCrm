using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crm_Project.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil...!")]
        public string ConfirmPassword { get; set; }
    }
}