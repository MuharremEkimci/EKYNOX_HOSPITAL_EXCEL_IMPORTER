using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Enums
{
    public enum EducationStatusEnum
    {
        [Display(Name = "Devam Ediyor")]
        InProgress = 0,
        [Display(Name = "Tamamlandı")]
        Completed = 1
    }
}
