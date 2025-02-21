using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarDetallePago
    {
        public int DetalleID { get; set; }
        public int PagoID { get; set; }
        public int CitaID { get; set; }
        public decimal Monto { get; set; }

        // Relaciones
        public Pago Pago { get; set; }
        public Cita Cita { get; set; }
    }
}