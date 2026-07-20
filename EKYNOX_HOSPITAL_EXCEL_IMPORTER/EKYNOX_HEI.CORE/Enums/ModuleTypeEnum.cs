using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Enums
{
    public enum ModuleTypeEnum
    {
        [Display(Name = "Vezne")]
        Vezne = 1,
        [Display(Name = "Gece Amirleri")]
        GeceAmirleri = 2,
        [Display(Name = "Fiyatlandırma")]
        Fiyatlandirma = 3,
        [Display(Name = "İnsan Kaynakları")]
        InsanKaynaklari = 4,
        [Display(Name = "Kurumsal Faturalandırma")]
        KurumsalFaturalandirma = 5,
        [Display(Name = "Medikal Muhasebe")]
        MedikalMuhasebe = 6,
        [Display(Name = "Hekim")]
        Hekim = 7,
        [Display(Name = "Asistan")]
        Asistan = 8,
        [Display(Name = "Hemşirelik Hizmetleri")]
        HemsirelikHizmetleri = 9,
        [Display(Name = "Eczane")]
        Eczane = 10,
        [Display(Name = "Depo")]
        Depo = 11,
        [Display(Name = "Satınalma")]
        Satinalma = 12,
        [Display(Name = "Stok")]
        Stok = 13,
        [Display(Name = "Laboratuvar")]
        Laboratuvar = 14,
        [Display(Name = "Radyoloji")]
        Radyoloji = 15,
    }
}
