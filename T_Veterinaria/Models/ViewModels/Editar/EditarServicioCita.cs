﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Veterinaria.Models.ViewModels.Editar
{
    public class EditarServicioCita
    {
        public int ServicioCitaID { get; set; }
        public int ServicioID { get; set; }
        public int CitaID { get; set; }
        public string Observacion { get; set; }
        public int EstadoID { get; set; }
    }
}