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
    
    public partial class Receta
    {
        public int RecetaID { get; set; }
        public int DiagnosticoID { get; set; }
        public int TratamientoID { get; set; }
        public string Indicaciones { get; set; }
    
        public virtual Diagnostico Diagnostico { get; set; }
        public virtual Tratamiento Tratamiento { get; set; }
    }
}
