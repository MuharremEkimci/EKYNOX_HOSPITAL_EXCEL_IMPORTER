using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Enums
{
    public enum ReadAndExcelProcessEnum
    {
        [Display(Name = "İşlem Yapılmadı")]
        NonProcess = 1,
        [Display(Name = "İşlem Yapıldı")]
        ProcessCompleted = 2,
        [Display(Name = "Hatalı Görüntü")]
        InvalidImage = 3
    }
}
