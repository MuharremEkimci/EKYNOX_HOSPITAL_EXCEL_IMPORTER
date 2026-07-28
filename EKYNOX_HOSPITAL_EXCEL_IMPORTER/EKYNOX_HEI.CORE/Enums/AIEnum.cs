using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Enums
{
    public enum AIEnum
    {
        [Display(Name = "Gemini")]
        Gemini = 1,
        [Display(Name = "Azure AI")]
        AzureAI = 2,
        [Display(Name = "Groq")]
        Groq = 3,
    }

    public enum AIEnumUsingStatus
    {
        [Display(Name = "Kullanımda")]
        Using = 1,
        [Display(Name = "Kullanım Dışı")]
        NotUsing = 2,

    }
}
