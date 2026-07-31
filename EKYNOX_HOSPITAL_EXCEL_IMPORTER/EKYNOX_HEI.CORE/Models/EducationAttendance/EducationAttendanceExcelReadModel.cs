using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{
    public class EducationAttendanceExcelReadModel
    {
        [Editable(false)]
        [DisplayName("Sıra No")]
        public int ClassNo { get; set; }

        [Editable(false)]
        [DisplayName("Adı")]
        public string? Name { get; set; }

        [Editable(false)]
        [DisplayName("Soyadı")]
        public string? Surname { get; set; }
    }
}
