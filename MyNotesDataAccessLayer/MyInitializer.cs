using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.EntityLayer;

namespace MyNotesDataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<MyNoteContext> // Seed dataarı yazmak için araya veri sokuşturmak için kullanıyoduk.MyNote contextinden türettiğim bir database yok ise
    {
        protected override void Seed(MyNoteContext context)
        {

            MyNotesUser admin = new MyNotesUser()
            {
                Name = "Efe",
                LastName = "Cemalcilar",
                Email = "efecemalcilar@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                ProfileImageFileName = "user.png",
                UserName = "ilhanity",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "system"

            };

            MyNotesUser stdUser = new MyNotesUser()
            {
                Name = "Recep",
                LastName = "Ivedik",
                Email = "recebim57@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                ProfileImageFileName = "user.png",
                UserName = "reco57",
                Password = "654321",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "system"


            };
            context.MyNotesUsers.Add(admin);
            context.MyNotesUsers.Add(stdUser);

            for (int i = 0; i < 8; i++)
            {
                MyNotesUser myUser = new MyNotesUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    LastName = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"user-{i}",
                    Password = "654321",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user-{i}"
                };
                context.MyNotesUsers.Add(myUser);

            }

            context.SaveChanges();
            List<MyNotesUser> userList = context.MyNotesUsers.ToList();

            for (int i = 0; i < 10; i++)
            {
                //Adding categories 
                Category cat = new Category()
                {
                    Tittle = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = "efecemalcilar"
                };

                context.Categories.Add(cat);

                //Adding fake Notes

                for (int j = 0; j < FakeData.NumberData.GetNumber(5, 9); j++)
                {
                    MyNotesUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];


                    Note note = new Note()
                    {
                        Tittle = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        isDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.UserName,
                    };

                    cat.Notes.Add(note);




                    //Adding Comment

                    for (int k = 0; k < FakeData.NumberData.GetNumber(3, 5); k++)
                    {
                        MyNotesUser comment_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        {
                            Comment comment = new Comment()
                            {
                                Text = FakeData.TextData.GetSentence(),
                                Owner = comment_owner,
                                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                                ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                                ModifiedUserName = comment_owner.UserName
                            };

                            note.Comments.Add(comment);

                        }
                        context.SaveChanges();



                    }

                    for (int l = 0; l < note.LikeCount; l++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[l]

                        };
                        note.Likes.Add(liked);
                    }

                }

            }
            context.SaveChanges();

        }
    }
}
