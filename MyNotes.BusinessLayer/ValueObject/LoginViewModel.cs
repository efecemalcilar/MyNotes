using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.BusinessLayer.ValueObject
{
    public class LoginViewModel
    {
        [DisplayName("Kullanici Adi"),Required(ErrorMessage = "{0} alani bos gecemezsiniz"),StringLength(30,ErrorMessage = "{0} max {1} karakter olmalı")]
        public string UserName { get; set; }

        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani bos gecemezsiniz"), StringLength(30, ErrorMessage = "{0} max {1} karakter olmalı"),DataType(DataType.Password)] // Datatype Password ü uygun şekilde kontrol edecek.
        public string Password { get; set; }
    }
}
