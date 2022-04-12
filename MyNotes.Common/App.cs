using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Common
{
    public static class App //Statric classın property si de static olmak zorundadır.
    {
        public static ICommon Common = new DefaultCommon();

    }
}
