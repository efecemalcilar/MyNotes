using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using MyNotes.MVC.Controllers;

namespace MyNotes.MVC.ViewModel
{
    public class NotifyViewModelBase<T> //Gelenecek her türlü hatayı karşılasın ve ona göre bir sonup üretsin
    {
        public List<T> Items { get; set; } = new List<T>(); //List tipinde T entitysini alan item isimli generic bir yapı

        public string Header { get; set; } = "Yonlendiriliyosunuz";

        public string Title { get; set; } = " Gecersiz Islem";

        public bool isRedirecting { get; set; } = true;

        public string RedirectingUrl { get; set; } = "/Home/Index";

        public int RedirectingTimeout { get; set; } = 3000;

        //public NotifyViewModelBase()
        //{
        //    Header = "Yönlendiriliyosunuz";
        //    Title = "Geçersiz Islem";
        //    isRedirecting = true;
        //    RedirectingUrl = "/Home/Index";
        //    RedirectingTimeout = 3000;
        //    Items = new List<>();

    }
}
