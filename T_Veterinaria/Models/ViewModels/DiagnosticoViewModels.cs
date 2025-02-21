using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class DiagnosticoViewModels
    {
        public int DiagnosticoID { get; set; }
        public int CitaID { get; set; }
        public string Descripcion { get; set; }

        // Relación con Cita
        public Cita Cita { get; set; }

        // Relación con Receta
        public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    }
}