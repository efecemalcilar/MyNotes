using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.BusinessLayer.ValueObject
{
    public class RegisterViewModel
    {
        [DisplayName("Adi"),Required(ErrorMessage = "{0} alani bos gecilemez"),StringLength(30,ErrorMessage = "{0} max {1} karakter olmali")]
        public string Name { get; set; }

        [DisplayName("Soyadi"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, ErrorMessage = "{0} max {1} karakter olmali")]
        public string LastName { get; set; }

        [DisplayName("KullaniciAdi"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, ErrorMessage = "{0} max {1} karakter olmali")]
        public string UserName { get; set; }

        [DisplayName("Email"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(100, ErrorMessage = "{0} max {1} karakter olmali"),EmailAddress(ErrorMessage = "{0} alani gecerli bir email adresi giriniz")]
        public string Email { get; set; }

        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30,MinimumLength = 3, ErrorMessage = "{0} max {1} min {2} karakter arasinda olmali"),DataType(DataType.Password),Compare("Password",ErrorMessage = "{0}{1} uyusmuyor...")]
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
