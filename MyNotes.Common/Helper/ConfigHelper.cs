using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Common.Helper
{
    public class ConfigHelper // Kodları uzun uzun yazmak yerine bazı yerlerde kümeleyip yardım aldığım dosyalar helper dosyalarıdır.Amacım bir configuarasyon dosyasından verilere ulaşmak istediğim için bu class ı kullanıcam
    {
        //public static string Get(string key)
        //{
        //    return ConfigurationManager.AppSettings[key];
        //}

        public static T Get<T>(string key) //587 numaralı port string yerine int ıstedıgı ıcın yukarıda kı yerıne bunu yazdık.
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        }
    }
}
