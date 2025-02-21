using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarPersonalViewModels
    {
        public int EmpleadoID { get; set; }
        public int? CargoId { get; set; }
        public string NombrePersona { get; set; }
        public string Apellido { get; set; }
        public int? Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string DNI { get; set; }


        public string Descripcion { get; set; }

        // Relación con Cargo
        public Cargo Cargo { get; set; }

        // Relación con Citas (1 Empleado puede atender muchas citas)
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
