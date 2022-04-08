using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using MyNotes.EntityLayer;

namespace MyNotes.BusinessLayer.Models
{
    public class CacheHelper // Her tarayıcıda olan bir özelliği aktif etmemi sağlayacak. Category her defasında karşıma cıkıyo ben bunun yerine bir kere Database den alacağım ve tarayıcının cach ine atacam. Cache i her çalıştırdığımda burdan calısaşacak.
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("categories"); //Cache imde bu nesne var mı diye kontrol ediyorum.
            if (result == null)
            {
                CategoryManager cm = new CategoryManager();
                result = cm.List();
                WebCache.Set("category-cache",result,30,true); //hafızasına aldığı category-cache'i 30 dk sonra siliyor.
            }

            return result;
        }
        public static void RemoveCategoriesFromCache() // Cache'i temizliyor.
        {
            WebCache.Remove("category-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}
