using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.BusinessLayer.Abstract;
using MyNotes.BusinessLayer.ValueObject;
using MyNotes.Common.Helper;
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

        public BusinessLayerResult<MyNotesUser> RegisterUser(RegisterViewModel data)
        {
            MyNotesUser user = Find(s => s.UserName == data.UserName || s.Email==data.Email);
            
            if (user!= null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExist, "Bu kullanici adi daha önce alinmis");
                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist,"Bu email adresi daha önce alinmis");
                }
            }
            else
            {
                int dbResult = base.Insert(new MyNotesUser()
                {
                    Name = data.Name,
                    LastName = data.LastName,
                    Email = data.Email,
                    UserName = data.UserName,
                    Password = data.Password,
                    IsActive = false,
                    IsAdmin = false,
                    ActivateGuid = Guid.NewGuid(),
                });

                if (dbResult>0)
                {
                    res.Result = Find(s => s.Email == data.Email && s.UserName == data.UserName);
                    //Activasyon maili gonderilecek 

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body =
                        $"Merhaba {res.Result.UserName};<br><br> Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'>Bu Linke Tiklayin</a>.";
                    MailHelper.SendMail(body, res.Result.Email, "MyNotes Hesabı Aktivasyon mail hizmeti");
                }
            }

            return res;
        }
    }
}
