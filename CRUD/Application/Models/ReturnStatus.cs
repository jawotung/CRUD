using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ReturnStatus
    {
        public string Message { get; set; }
        public int Status { get; set; }
    }
    public class ReturnStatusData<T> : ReturnStatus
    {
        public T Data { get; set; }
    }
    public class ReturnStatusList<T> : ReturnStatus
    {
        public List<T> Data { get; set; }
    }
}
