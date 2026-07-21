using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.Institutions
{
    public class InstitutionsViewModel
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Editable(false)]
        [DisplayName("Kurum Kodu")]
        public string? Code { get; set; }

        [Editable(false)]
        [DisplayName("Kurum Adı")]
        public string? Name { get; set; }

        [Browsable(false)]
        public string? City { get; set; }

        [Browsable(false)]
        public string? Town { get; set; }

        [Browsable(false)]
        public string? District { get; set; }

        [Browsable(false)]
        public string? Address { get; set; }
    }
}
