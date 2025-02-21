using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels
{
    public class ListarServicioViewModels
    {
        public int ServicioID { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int DuracionMinutos {  get; set; } 
    }
}