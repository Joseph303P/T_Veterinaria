using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.New
{
    public class NuevaReceta
    {
        public int RecetaID { get; set; }
        public int DiagnosticoID { get; set; }
        public int TratamientoID { get; set; }
        public string Indicaciones { get; set; }
    }
}