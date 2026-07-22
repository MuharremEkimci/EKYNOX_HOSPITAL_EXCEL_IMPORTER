using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.Users
{
    public class UsersViewModel
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Editable(false)]
        [DisplayName("Kullanıcı No")]
        public int Nr { get; set; }

        [Editable(false)]
        [DisplayName("Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Editable(false)]
        [DisplayName("Ad")]
        public string? Name { get; set; }

        [Editable(false)]
        [DisplayName("Soyad")]
        public string? Surname { get; set; }

        [Editable(false)]
        [DisplayName("Rol")]
        public RoleEnum Role { get; set; }

        [Browsable(false)]
        public string? EMail { get; set; }

        [Browsable(false)]
        public string? Phone { get; set; }
    }
}
