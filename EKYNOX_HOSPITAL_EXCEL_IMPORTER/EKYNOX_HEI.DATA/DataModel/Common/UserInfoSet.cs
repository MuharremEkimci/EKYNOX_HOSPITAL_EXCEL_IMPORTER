using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.DataModel.Common
{
    public class UserInfoSet
    {
        public int LogicalRef { get; set; }
        public int Nr { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public RoleEnum Role { get; set; }
        public string? EMail { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}
