using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class PagoViewModels
    {
        public int PagoID { get; set; }
        public int MetodoID { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }

        public string Nombre { get; set; }


        // Relación con MetodoPago
        public Metodo_Pago MetodoPago { get; set; }
    }
}