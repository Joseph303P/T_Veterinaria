using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarMascotaViewModels
    {
        public int MascotaID { get; set; }
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int? Edad { get; set; }


        public string NombreCliente { get; set; }
        // Relación con Cliente
        public Cliente Cliente { get; set; }

        // Relación con Cita
        public ICollection<Cita> Citas { get; set; }
    }
}
    
