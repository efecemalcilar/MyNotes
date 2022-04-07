using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotes.MVC.ViewModel
{
    public class OkViewModel : NotifyViewModelBase<string> //Entity string tipinde dönüş yapsın demek. Item bana string tipinde geri dönüş yapsın demek oluyor. Bu class sadece Title değiştirebilmek için yazdık.
    {
        public OkViewModel()
        {
            Title = "Islem Basarili";

        }
    }
}