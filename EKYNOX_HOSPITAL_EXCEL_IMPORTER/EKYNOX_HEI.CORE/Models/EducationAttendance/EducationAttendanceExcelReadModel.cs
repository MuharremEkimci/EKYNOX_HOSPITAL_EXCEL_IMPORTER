using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{
    public class EducationAttendanceExcelReadModel
    {
        [Browsable(false)]
        //[ReadOnly(true)]
        //[DisplayName("Sıra No")]
        public int ClassNo { get; set; }

        [ReadOnly(true)]
        [DisplayName("Adı")]
        public string? Name { get; set; }

        [ReadOnly(true)]
        [DisplayName("Soyadı")]
        public string? Surname { get; set; }
    }
}
