using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{
    public class EducationAttendanceModel
    {
        [DisplayName("Dosya Adı")]
        public string? FileName { get; set; }

        [DisplayName("Dosya Yolu")]
        public string? FilePath { get; set; }

        [DisplayName("Dosya Verisi")]
        public byte[]? FileData { get; set; }

        [DisplayName("Eğitim Tarihi")]
        public DateTime EducationDate { get; set; }

        [DisplayName("Eğitim Türü")]
        public EducationTypeEnum EducationType { get; set; }

        [DisplayName("Modül")]
        public ModuleTypeEnum Module { get; set; }

        [DisplayName("Detay")]
        public List<EducationAttendanceDetailModel> Detail { get; set; }

        public EducationAttendanceModel() => Detail = new List<EducationAttendanceDetailModel>();
    }

    public class EducationAttendanceDetailModel
    {
        [DisplayName("Sıra No")]
        public int ClassNo { get; set; }

        [DisplayName("Adı Soyadı")]
        public string? FullName { get; set; }
    }
}
