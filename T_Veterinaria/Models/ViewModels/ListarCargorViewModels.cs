using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarCargorViewModels
    {
        public int CargoId { get; set; }
        public string Descripcion { get; set; }

        // Relación con Personal (1 Cargo puede estar asociado a muchos empleados)
        public ICollection<Personal> Empleados { get; set; } = new List<Personal>();
    }
}