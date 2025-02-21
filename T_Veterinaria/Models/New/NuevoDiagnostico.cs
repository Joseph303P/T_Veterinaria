using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.New
{
    public class NuevoDiagnostico
    {
        public int DiagnosticoID { get; set; }
        public int CitaID { get; set; }
        public string Descripcion { get; set; }
    }
}