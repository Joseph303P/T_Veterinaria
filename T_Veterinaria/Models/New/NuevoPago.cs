using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.New
{
    public class NuevoPago
    {
        public int PagoID { get; set; }
        public int MetodoID { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
    }
}