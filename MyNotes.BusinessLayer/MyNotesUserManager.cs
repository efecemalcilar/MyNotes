using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.BusinessLayer.Abstract;
using MyNotes.BusinessLayer.ValueObject;
using MyNotes.EntityLayer;
using MyNotes.EntityLayer.Messages;


namespace MyNotes.BusinessLayer
{
    public class MyNotesUserManager:ManagerBase<MyNotesUser>
    {
        // Kullanici username kontrolu yapmaliyim
        // Kullanici email kontrolü yapmaliyim.
        // Kayit islemi gerceklestirmeliyim
        // Aktivation e-posta gönderimi yapmalıyım

        private BusinessLayerResult<MyNotesUser> res = new BusinessLayerResult<MyNotesUser>();

        public BusinessLayerResult<MyNotesUser> LoginUser(LoginViewModel data) //Bu metod dışarıdan parametre almalı bu parametre içerisinde username email şifre olmalı
        {
            res.Result = Find(s => s.UserName == data.UserName && s.Password==data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive) //Burasi false gelme durumu.
                { 
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanici adi aktiflesti");
                    res.AddError(ErrorMessageCode.CheckYourEmail,"Lutfen mailinizi kontrol ediniz.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanici adi yada sifreniz yanlis lutfen kontrol edin...");
            }

            return res;
        }
    }
}
