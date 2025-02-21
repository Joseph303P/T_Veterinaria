using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarClienteViewModels
    {
        public int ClienteID { get; set; }
        public string NombreCliente { get; set; }
        public string Apellido { get; set; }
        public int? Telefono { get; set; } // Hacemos Telefono nullable en caso de que no sea obligatorio
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string DNI { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
    }
}
