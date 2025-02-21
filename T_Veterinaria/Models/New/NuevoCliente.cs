using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.New
{
    public class NuevoCliente
    {
        public int ClienteID { get; set; }
        public string NombreCliente { get; set; }
        public string Apellido { get; set; }
        public int? Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string DNI { get; set; }
    }
}
