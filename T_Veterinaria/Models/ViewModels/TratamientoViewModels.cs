using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class TratamientoViewModels
    {
        public int TratamientoID { get; set; }
        public string Detalles { get; set; }

        // Relación con Receta
        public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    }
}