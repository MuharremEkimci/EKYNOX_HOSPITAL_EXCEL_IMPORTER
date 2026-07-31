using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.CORE.Helpers
{
    public class ReturnData<T>
    {
        public T Data { get; set; }
        public StatusEnum Status { get; set; }
        public string Message { get; set; }

        public ReturnData() => Status = StatusEnum.Success;
    }
}
