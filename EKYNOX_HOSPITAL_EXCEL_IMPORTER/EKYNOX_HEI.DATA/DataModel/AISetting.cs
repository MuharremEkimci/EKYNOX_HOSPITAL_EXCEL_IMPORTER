using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.DATA.DataModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.DataModel
{
    public class AISetting : RowInfoModel
    {
        public int LOGICALREF { get; set; }
        public string? AINO { get; set; }
        public AIEnum AI { get; set; }
        public string? APIKEY { get; set; }
        public AIEnumUsingStatus USINGSTATUS { get; set; }
        public string? METHODNAME { get; set; }
        public string? ENDPOINT { get; set; }
    }

    public class AISettingDetail : RowInfoModel
    {
        public int LOGICALREF { get; set; }
        public int AISETTINGREF { get; set; }
        public int LINENR { get; set; }
        public string? AIMODELNAME { get; set; }
        public string? AIMODELDESC { get; set; }
        public bool USEINTHEMETHOD { get; set; }
    }
}
