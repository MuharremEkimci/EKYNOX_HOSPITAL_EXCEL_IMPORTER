using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.AISetting
{
    public class AISettingListViewModel
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Editable(false)]
        [DisplayName("Yapay Zeka")]
        public AIEnum Ai { get; set; }

        [Editable(false)]
        [DisplayName("Api Key")]
        public string? ApiKey { get; set; }

        [Editable(false)]
        [DisplayName("Method")]
        public string? MethodName { get; set; }

        [Editable(false)]
        [DisplayName("Kullanım Durumu")]
        public AIEnumUsingStatus UsingStatus { get; set; }
    }
}
