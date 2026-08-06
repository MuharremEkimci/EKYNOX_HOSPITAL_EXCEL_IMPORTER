using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{
    public class EducationAttendanceListViewModel
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Editable(false)]
        [DisplayName("Döküman No")]
        public string? DocNo { get; set; }

        [Editable(false)]
        [DisplayName("Eğitmen")]
        public string? Educator { get; set; }

        [Editable(false)]
        [DisplayName("Kurum Kodu")]
        public string? InstitutionCode { get; set; }

        [Editable(false)]
        [DisplayName("Kurum Adı")]
        public string? InstitutionName { get; set; }

        [Editable(false)]
        [DisplayName("Eklenme Tarihi")]
        public DateTime CreatedDate { get; set; }

        [Editable(false)]
        [DisplayName("Değiştirme Tarihi")]
        public DateTime ModifiedDate { get; set; }
    }
}
