using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.DataModel.Common
{
    public class RowInfoModel
    {
        public DateTime CREATEDATE { get; set; }
        public DateTime MODIFIEDDATE { get; set; }
        public int CREATEDUSER { get; set; }
        public int MODIFIEDUSER { get; set; }
    }
}
