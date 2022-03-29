using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.EntityLayer.Messages
{
    public class ErrorMessageObj // Hata mesajlarını için obje  oluşturacak ve ben bunu businessLayer da manager de işlem yapıcam , kullanıcı adı ve şifreyi kontrol edecek varsa problem yok ama yoksa kullanıcıya mesaj iletecek.  
    {
        public ErrorMessageCode Code { get; set; }
        public string Message { get; set; }

    }
}
