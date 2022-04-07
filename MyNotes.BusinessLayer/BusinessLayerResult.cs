using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.EntityLayer;
using MyNotes.EntityLayer.Messages;

namespace MyNotes.BusinessLayer
{
    public class BusinessLayerResult<T> where T: class //Bunun bütün entity varlıklarda geçerli olması için T tipinde generic hale getiriyorum.
    {
        public List<ErrorMessageObj> Errors { get; set; } 

        public T Result { get; set; } //BusinessLAyerResult un 2 tip dönüş değeri var

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddError(ErrorMessageCode code, string message) //Business tarafında add error metodunu kullanıcam
        {
            Errors.Add(new ErrorMessageObj{Code = code , Message = message});

        }
    }
}
