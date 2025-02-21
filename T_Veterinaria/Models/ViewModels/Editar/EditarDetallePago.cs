using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels.Editar
{
    public class EditarDetallePago
    {
        public int DetalleID { get; set; }
        public int PagoID { get; set; }
        public int CitaID { get; set; }
        public decimal Monto { get; set; }
    }
}