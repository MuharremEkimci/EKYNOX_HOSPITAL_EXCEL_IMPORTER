using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{
    public class EducationAttendanceListViewModel
    {
        public int LogicalRef { get; set; }

        [DisplayName("Döküman No")]
        public string? DocNo { get; set; }

        [DisplayName("Eğitmen")]
        public string? Educator { get; set; }

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
