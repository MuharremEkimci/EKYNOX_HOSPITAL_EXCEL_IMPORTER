using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Enums
{
    public enum RoleEnum
    {
        [Display(Name = "Kullanıcı")]
        User = 1,
        [Display(Name = "Yönetici")]
        Admin = 2,
    }
}
