using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T_Veterinaria.Models;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarCitasViewModels
    {
        public int CitaID { get; set; }
        public int MascotaID { get; set; }
        public int EmpleadoID { get; set; }
        public int? ServicioID { get; set; }

        public string NombreServicio { get; set; }

        public DateTime FechaCita { get; set; }
        public int EstadoID { get; set; }



        public string Nombre { get; set; }

        public string NombrePersona { get; set; }

        public string Descripcion { get; set; }
        



        // Relaciones
        public Mascota Mascota { get; set; }
        public Personal Empleado { get; set; }
        public Estado Estado { get; set; }

        // Relación con Diagnostico, Detalle_Pago 
        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
        public ICollection<Detalle_Pago> DetallesPago { get; set; } = new List<Detalle_Pago>();
    }
}