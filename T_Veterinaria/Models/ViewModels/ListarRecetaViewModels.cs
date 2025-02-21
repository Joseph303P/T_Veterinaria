using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarRecetaViewModels
    {
        public int RecetaID { get; set; }
        public int DiagnosticoID { get; set; }
        public int TratamientoID { get; set; }
        public string Indicaciones { get; set; }

        //Datos Compartidos
        //Diagnostico
        public string Descripcion { get; set; }
        //Tratamiento
        public string Detalles { get; set; }


        // Relaciones
        public Diagnostico Diagnostico { get; set; }
        public Tratamiento Tratamiento { get; set; }
    }
}