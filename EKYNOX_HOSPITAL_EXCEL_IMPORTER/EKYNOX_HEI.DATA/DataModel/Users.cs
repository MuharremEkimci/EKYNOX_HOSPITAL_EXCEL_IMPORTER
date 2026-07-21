using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.DataModel
{
    public class Users
    {
        public int LOGICALREF { get; set; }
        public int NR { get; set; }
        public string? USERNAME { get; set; }
        public string? NAME { get; set; }
        public string? SURNAME { get; set; }
        public string? PASSWORD { get; set; }
        public string? EMAIL { get; set; }
        public string? PHONE { get; set; }
        public RoleEnum ROLE { get; set; }
    }
}
