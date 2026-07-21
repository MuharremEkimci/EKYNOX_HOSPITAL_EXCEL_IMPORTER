using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Models.Institutions
{
    public class InstitutionsModel
    {
        public int LogicalRef { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? District { get; set; }
        public string? Address { get; set; }
    }
}
