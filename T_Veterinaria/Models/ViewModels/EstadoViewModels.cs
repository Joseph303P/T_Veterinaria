using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class EstadoViewModels
    {
        public int EstadoID { get; set; }
        public string Descripcion { get; set; }

        // Relación con Citas
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}