//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace T_Veterinaria.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Detalle_Pago
    {
        public int DetalleID { get; set; }
        public int PagoID { get; set; }
        public int CitaID { get; set; }
        public decimal Monto { get; set; }
    
        public virtual Cita Cita { get; set; }
        public virtual Pago Pago { get; set; }
    }
}
