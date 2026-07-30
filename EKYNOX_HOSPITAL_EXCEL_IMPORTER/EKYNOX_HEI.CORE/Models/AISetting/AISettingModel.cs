using EKYNOX_HEI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.AISetting
{
    public class AISettingModel
    {
        public int LogicalRef { get; set; }
        public string? AiNo { get; set; }
        public AIEnum Ai { get; set; }
        public string? ApiKey { get; set; }
        public AIEnumUsingStatus UsingStatus { get; set; }
        public string? MethodName { get; set; }
        public string? Endpoint { get; set; }
        public List<AISettingListModel> Detail { get; set; }

        public AISettingModel() => Detail = new List<AISettingListModel>();
    }

    public class AISettingListModel 
    {
        [Browsable(false)]
        public int LogicalRef { get; set; }

        [Browsable(false)]
        public int AISettingRef { get; set; }

        [Editable(false)]
        [DisplayName("Sıra No")]
        public int? LineNr { get; set; }

        [DisplayName("Model Adı")]
        public string? AiModelName { get; set; }

        [Editable(false)]
        [DisplayName("Model Açıklama")]
        public string? AiModelDesc { get; set; }

        [DisplayName("Seçilen Methodda Modeli Kullan")]
        public bool UseInTheMethod { get; set; }

        [DisplayName("Model Test")]
        public string? AiModelTest { get; set; }
    }
}
