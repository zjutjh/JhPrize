using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhPrize.Core
{
    public class ResponseModel<T>
    {
        public int code;
        public string msg;
        public T data;
    }
}
