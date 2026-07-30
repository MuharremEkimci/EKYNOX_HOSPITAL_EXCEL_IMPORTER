using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{

    public class EducationAttendanceModel
    {
        public int LogicalRef { get; set; }
        public string DocNo { get; set; }
        public int EducatorRef { get; set; }
        public int InstitutionRef { get; set; }

        public List<EducationAttendanceListModel> ImagesDetailList { get; set; }
        public EducationAttendanceModel() 
        {
            ImagesDetailList = new List<EducationAttendanceListModel>();
        }
    }

    public class EducationAttendanceListModel
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Browsable(false)]
        public string FileMimeType { get; set; }

        [Editable(false)]
        [DisplayName("Dosya Adı")]
        public string? FileName { get; set; }

        [Editable(false)]
        [DisplayName("Dosya Yolu")]
        public string? FilePath { get; set; }

        [Editable(false)]
        [DisplayName("Dosya Verisi")]
        public byte[]? FileData { get; set; }

        [DisplayName("Eğitim Tarihi")]
        public DateTime EducationDate { get; set; }

        [DisplayName("Eğitim Türü")]
        public EducationTypeEnum EducationType { get; set; }

        [DisplayName("Modül")]
        public ModuleTypeEnum ModuleType { get; set; }

        [DisplayName("Kaçıncı Eğitim ?")]
        public int EducationNumber { get; set; }

        [Editable(false)]
        [DisplayName("Durum")]
        public ReadAndExcelProcessEnum ReadAndExcelProcess { get; set; }

        [Browsable(false)]
        [DisplayName("Detay")]
        public List<EducationAttendanceDetailModel> Detail { get; set; }

        public EducationAttendanceListModel() => Detail = new List<EducationAttendanceDetailModel>();
    }

    public class EducationAttendanceDetailModel
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Editable(false)]
        [DisplayName("Sıra No")]
        public int ClassNo { get; set; }

        [DisplayName("Adı")]
        public string? Name { get; set; }

        [DisplayName("Soyadı")]
        public string? Surname { get; set; }
    }
}
