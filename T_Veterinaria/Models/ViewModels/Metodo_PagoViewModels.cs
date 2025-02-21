using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class Metodo_PagoViewModels
    {
        public int MetodoID { get; set; }
        public string Nombre { get; set; }

        // Relación con Pago (1 Metodo_Pago puede estar asociado a muchos Pagos)
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}