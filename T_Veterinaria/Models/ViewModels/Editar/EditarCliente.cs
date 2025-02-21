using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels.Editar
{
    public class EditarCliente
    {
        public int? ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string DNI { get; set; }
    }
}