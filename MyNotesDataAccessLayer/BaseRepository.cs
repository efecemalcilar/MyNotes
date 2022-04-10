using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotesDataAccessLayer
{
    public class BaseRepository
    {
        private static MyNoteContext _db;
        private static object _lock = new object(); // Bir kere oluştuysa bunu kitleyeceğim.

        public BaseRepository()
        {

        }

        public static MyNoteContext CreateContext() // db null ise lockla ve içini doldur bana bas. db içi null değilse zaten buraya hiç girmiyorum olanı kullanmış oluyorum.
        {
            if (_db==null)
            {
                lock (_lock) //2. defa oluşmasını engelliyor ama farklı bir context varsa ona karışma demek için lockluyorum.
                {
                    if (_db == null)
                    {
                        _db=new MyNoteContext();
                    }
                }
            }

            return _db;
        }
    }
}
