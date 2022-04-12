using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            res.Result = Find(s => s.UserName == data.UserName && s.Password==data.Password && s.IsDelete != true);

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
            MyNotesUser user = Find(s => s.UserName == data.UserName );
            //|| s.Email==data.Email
            if (user!= null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExist, "Bu kullanici adi daha önce alinmis");
                }

                //if (user.Email == data.Email)
                //{
                //    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu email adresi daha önce alinmis");
                //}
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
                    ProfileImageFileName = "user.png"
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

        public BusinessLayerResult<MyNotesUser> SendMail(LoginViewModel data)
        {
            res.Result = Find(s => s.Password == data.Password && s.UserName == data.UserName
            );
            //Activasyon maili gonderilecek

            string siteUri = ConfigHelper.Get<string>("SiteRootUri");
            string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
            string body =
                $"Merhaba {res.Result.UserName};<br><br> Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'>bu linke tiklayin</a>.";
            MailHelper.SendMail(body, res.Result.Email, "MyNotes Aktivasyon mail hizmet");

            return res;

        }

        public BusinessLayerResult<MyNotesUser> ActivateUser(Guid id)
        {
            res.Result = Find(s => s.ActivateGuid == id);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive,"Kullanici zaten aktif");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExist," Boyle Bir aktivasyon kodu yoktur");
                
            }

            return res;




        }

        public new BusinessLayerResult<MyNotesUser> Insert(MyNotesUser data)
        {
            MyNotesUser user = Find(s => s.UserName == data.UserName || s.Email == data.Email);
            res.Result = data;

            if (user != null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExist,"Bu Kullanici adi daha önce alinmis.");
                }

                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu email  daha önce alinmis.");
                }


            }

            else
            {
                res.Result.ActivateGuid = Guid.NewGuid(); //Yeni bir global userid oluşturmasını istiyorum
                if (base.Insert(res.Result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanici eklenemedi");
                }
            }

            return res;
        }

        public new BusinessLayerResult<MyNotesUser> Update(MyNotesUser data)
        {
            MyNotesUser user = Find(s => s.Id != data.Id && (s.UserName == data.UserName) || s.Email == data.Email);
            if (user != null && user.Id != data.Id)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExist,"Bu kullanici adini alamazsiniz..."); 
                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu email adresini  alamazsiniz...");
                }

                return res;
            }

            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.LastName = data.LastName;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsAdmin = data.IsAdmin;
            res.Result.IsActive = data.IsActive;

            if (base.Update(res.Result)==0) //Base gelme sebebi , ana işlemim ManagerBase de ki update işlemi normalde yapması gereken update çağrıldıgında ManagerBase den cagirilacak. 
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated,"Kullanici bilgileri guncellenemedi");
            }

            return res;
        }

        public BusinessLayerResult<MyNotesUser> RemoveUserById(int id)
        {
           res.Result= Find(s => s.Id == id);
            if (res.Result != null)
            {


                res.Result.IsDelete = true;

                if (base.Update(res.Result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove,"Kullanici silinemedi.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind,"Kullanici bulunamadi.");
            }

            return res;
        }

        public BusinessLayerResult<MyNotesUser> GetUserById(int id)
        {
            res.Result = Find(s => s.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound,"Kullanici bulunamadi..");
            }

            return res;
        }

        public BusinessLayerResult<MyNotesUser> UpdateProfile(MyNotesUser data)
        {
            MyNotesUser user = Find(s => s.Id != data.Id && (s.UserName == data.UserName || s.Email == data.Email));
            if (user != null && user.Id != data.Id)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Bu kullanici adi daha once kaydedilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu email daha once kaydedilmis.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.LastName = data.LastName;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsDelete = data.IsDelete;
            if (!string.IsNullOrEmpty(data.ProfileImageFileName))
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil guncellenemedi.");
            }

            return res;
        }
    }

}
