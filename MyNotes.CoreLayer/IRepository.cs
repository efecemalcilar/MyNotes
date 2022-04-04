using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.CoreLayer
{
    public interface IRepository<T> // Bu T hepsini ortak olarak kullanacak.
    {
        List<T> List();
        List<T> List(Expression<Func< T, bool >> predicate); //Bana T tipinde bir nesne gönder ben sana bool tipinde sonuç türeteyim diyor. select * from table where id=1 from kısmından sonrası anlamına geliyor.
        IQueryable<T> QList();
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
        int Save();
        T Find(Expression<Func< T, bool>> predicate);

    }
}
