using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.BusinessLayer.Abstract;
using MyNotes.BusinessLayer.ValueObject;
using MyNotes.EntityLayer;
using MyNotesDataAccessLayer;

namespace MyNotes.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category> // Category e tıkladığımda notları gelsin istiyorum.
    {
        private Repository<Category> repo = new Repository<Category>();

        public List<Category> IndexCat()
        {
            return repo.List();
        }

        public int InsertCat(CategoryViewModel Cat)
        {
            Category entity = new Category();
            entity.Tittle = Cat.Category.Tittle;
            entity.Description = Cat.Category.Description;
            //entity.CreatedOn = null;
            //entity.ModifiedUserName = null;
            //entity.ModifiedOn = null;
            
            return repo.Insert(entity);
        }

        public CategoryViewModel FindCat(int? id)
        {


            var cat = repo.QList().FirstOrDefault(x => x.Id == id);

            CategoryViewModel cvm = new CategoryViewModel();

            cvm.Category.Id = cat.Id;
            cvm.Category.Tittle = cat.Tittle;
            cvm.Category.Description = cat.Description;
            cvm.Category.CreatedOn = cat.CreatedOn;
            cvm.Category.ModifiedUserName = cat.ModifiedUserName;
            cvm.Category.ModifiedOn = cat.ModifiedOn;
            



            return cvm;
        }
    }
}
