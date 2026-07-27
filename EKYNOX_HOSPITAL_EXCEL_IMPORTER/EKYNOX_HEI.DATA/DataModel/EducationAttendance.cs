using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EKYNOX_HEI.DATA.DataModel
{
    public class EducationAttendance
    {
        public int LOGICALREF { get; set; }        
        public string? DOCNO { get; set; }
        public int EDUCATORREF { get; set; }
        public int INSTUTIONREF { get; set; }
        public EducationStatusEnum EDUCATIONSTATUS { get; set; }
    }

    public class EducationAttendanceDetail
    {
        public int LOGICALREF { get; set; }
        public int EDUCATIONATTENDANCEREF { get; set; }
        public string? FILENAME { get; set; }
        public string? FILEPATH { get; set; }
        public byte[]? FILEDATA { get; set; }
        public DateTime EDUCATIONDATE { get; set; }
        public EducationTypeEnum EDUCATIONTYPE { get; set; }
        public ModuleTypeEnum MODULETYPE { get; set; }
        public int EDUCATIONNUMBER { get; set; }
        public ReadAndExcelProcessEnum READANDEXCELPROCESS { get; set; }
    }

    public class EducationAttendanceFileRead
    {
        public int LOGICALREF { get; set; }
        public int EDUCATIONATTENDANCEDETAILREF { get; set; }
        public int CLASSNO { get; set; }
        public string? NAME { get; set; }
        public string? SURNAME { get; set; }
    }
}
