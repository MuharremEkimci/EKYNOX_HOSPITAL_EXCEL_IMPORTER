using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.DataModel
{
    public class EducationAttendance
    {
        public int LOGICALREF { get; set; }
        public int INSTUTIONREF { get; set; }
        public string? DOCNO { get; set; }
        public string? EDUCATIONFULLNAME { get; set; }
        public DateTime DATE_ { get; set; }        
        public string? FILEPATH { get; set; }
        public string? FILENAME { get; set; }
        public EducationStatusEnum EDUCATIONSTATUS { get; set; }
    }
}
