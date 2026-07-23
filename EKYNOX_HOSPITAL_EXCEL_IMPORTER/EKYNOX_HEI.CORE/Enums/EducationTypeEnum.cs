using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Enums
{
    public enum EducationTypeEnum
    {
        [Display(Name = "Eğitim")]
        Education = 1,
        [Display(Name = "Simülasyon")]
        Simulation = 2
    }
}
