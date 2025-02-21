using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels.Editar
{
    public class EditarCitas
    {
        public int CitaID { get; set; }
        public int MascotaID { get; set; }
        public int EmpleadoID { get; set; }
        public DateTime FechaCita { get; set; }
        public int? ServicioID { get; set; }
        public int EstadoID { get; set; }
    }
}