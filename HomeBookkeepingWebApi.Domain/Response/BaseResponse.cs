using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T? Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string>? ErrorMessages { get; set; }
    }
    public interface IBaseResponse<T>
    {
        public T? Result { get; set; }
    }
}
