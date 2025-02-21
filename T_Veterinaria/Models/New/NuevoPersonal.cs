using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.New
{
    public class NuevoPersonal
    {
        public int EmpleadoID { get; set; }
        public int CargoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string DNI { get; set; }
    }
}
