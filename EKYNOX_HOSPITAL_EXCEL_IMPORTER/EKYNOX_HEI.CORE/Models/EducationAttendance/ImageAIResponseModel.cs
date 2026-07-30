using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.EducationAttendance
{
    public class ImageAIResponseModel
    {
        public List<ImageAIResponseSubModel> participants { get; set; }

        public ImageAIResponseModel() =>  participants = new List<ImageAIResponseSubModel>();
    }

    public class ImageAIResponseSubModel
    {
        public int class_no { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
    }
}
