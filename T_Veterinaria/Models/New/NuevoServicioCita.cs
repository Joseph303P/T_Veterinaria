using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.New
{
    public class NuevoServicioCita
    {
        public int ServicioCitaID { get; set; }
        public int ServicioID { get; set; }
        public int CitaID { get; set; }
        public int Observacion { get; set; }
        public int EstadoID { get; set; }
    }
}