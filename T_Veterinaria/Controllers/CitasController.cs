using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_Veterinaria.Models.ViewModels.Editar;
using T_Veterinaria.Models.ViewModels;
using T_Veterinaria.Models;

namespace T_Veterinaria.Controllers
{
    public class CitasController : Controller
    {
        // GET: Citas
        public ActionResult Citas(string citaIDFiltro)
        {
            List<ListarCitasViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var query = db.Cita.AsQueryable();

                if (!string.IsNullOrEmpty(citaIDFiltro))
                {
                    query = query.Where(c => c.CitaID.ToString().Contains(citaIDFiltro)); // Filtro por CitaID
                }

                lts = query.Select(d => new ListarCitasViewModels
                {
                    CitaID = d.CitaID,
                    MascotaID = d.MascotaID,
                    EmpleadoID = d.EmpleadoID,
                    FechaCita = d.FechaCita,
                    ServicioID = d.ServicioID,
                    EstadoID = d.EstadoID
                }).ToList();
            }

            return View(lts);
        }


        public ActionResult NuevoCitas()
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Obtener mascotas que no están asociadas a citas activas
                var mascotasDisponibles = db.Mascota
                    .Where(m => !db.Cita.Any(c => c.MascotaID == m.MascotaID))
                    .Select(m => new SelectListItem
                    {
                        Value = m.MascotaID.ToString(),
                        Text = m.Nombre
                    }).ToList();

                ViewBag.MascotasDisponibles = mascotasDisponibles;

                // Obtener empleados para la lista desplegable
                var empleados = db.Personal.Select(e => new SelectListItem
                {
                    Value = e.EmpleadoID.ToString(),
                    Text = e.Nombre + " " + e.Apellido
                }).ToList();

                ViewBag.EmpleadosDisponibles = empleados;

                // Obtener servicios disponibles para las citas
                var servicios = db.Servicio.Select(s => new SelectListItem
                {
                    Value = s.ServicioID.ToString(),
                    Text = s.Nombre
                }).ToList();

                ViewBag.ServiciosDisponibles = servicios;

                // Obtener Estado
                var Estado = db.Estado.Select(o => new SelectListItem
                {
                    Value = o.EstadoID.ToString(),
                    Text = o.Descripcion
                }).ToList();

                ViewBag.Estado_Cita = Estado;
            }
            return View();
        }




        [HttpPost]
        public ActionResult NuevoCitas(ListarCitasViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oCitas = new Cita();
                        oCitas.CitaID = model.CitaID;
                        oCitas.MascotaID = model.MascotaID;
                        oCitas.EmpleadoID = model.EmpleadoID;
                        oCitas.FechaCita = model.FechaCita;
                        oCitas.ServicioID = model.ServicioID;
                        oCitas.EstadoID = model.EstadoID;

                        db.Cita.Add(oCitas);
                        db.SaveChanges();
                    }
                    return Redirect("Citas");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarCitas(int? id)
        {
            EditarCitas model = new EditarCitas();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oCitas = db.Cita.Find(id);
                model.MascotaID = oCitas.MascotaID;
                model.EmpleadoID = oCitas.EmpleadoID;
                model.FechaCita = oCitas.FechaCita;
                model.ServicioID = oCitas.ServicioID;
                model.EstadoID = oCitas.EstadoID;
                model.CitaID = oCitas.CitaID;

                // Obtener empleados para el dropdown
                var empleados = db.Personal.Select(e => new SelectListItem
                {
                    Value = e.EmpleadoID.ToString(),
                    Text = e.Nombre + " " + e.Apellido
                }).ToList();

                // Obtener servicios para el dropdown
                var servicios = db.Servicio.Select(s => new SelectListItem
                {
                    Value = s.ServicioID.ToString(),
                    Text = s.Nombre
                }).ToList();

                // Obtener estados para el dropdown
                var estados = db.Estado.Select(o => new SelectListItem
                {
                    Value = o.EstadoID.ToString(),
                    Text = o.Descripcion
                }).ToList();

                // Pasar las listas a ViewBag para el dropdown
                ViewBag.EmpleadosDisponibles = empleados;
                ViewBag.ServiciosDisponibles = servicios;
                ViewBag.Estado_Cita = estados;
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult EditarCitas(EditarCitas model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oCitas = db.Cita.Find(model.CitaID);
                        oCitas.MascotaID = model.MascotaID;
                        oCitas.EmpleadoID = model.EmpleadoID;
                        oCitas.FechaCita = model.FechaCita;
                        oCitas.ServicioID = model.ServicioID;
                        oCitas.EstadoID = model.EstadoID;

                        db.Entry(oCitas).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Citas/Citas");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarCita(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Eliminar diagnósticos asociados a la cita
                var diagnosticos = db.Diagnostico.Where(d => d.CitaID == id).ToList();
                foreach (var diagnostico in diagnosticos)
                {
                    // Eliminar recetas asociadas al diagnóstico
                    var recetas = db.Receta.Where(r => r.DiagnosticoID == diagnostico.DiagnosticoID).ToList();
                    db.Receta.RemoveRange(recetas);

                    db.Diagnostico.Remove(diagnostico);
                }

                // Eliminar detalles de pago asociados a la cita
                var detallesPago = db.Detalle_Pago.Where(dp => dp.CitaID == id).ToList();
                db.Detalle_Pago.RemoveRange(detallesPago);

                // Eliminar la cita
                var cita = db.Cita.Find(id);
                if (cita != null)
                {
                    db.Cita.Remove(cita); // Eliminar la cita
                }

                // Guardar cambios en la base de datos
                db.SaveChanges();
            }

            // Redirigir a la lista de citas
            return Redirect("~/Citas/Citas");
        }

        public ActionResult Citas_Person_Masc()
        {
            List<ListarCitasViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Cita
                       join m in db.Mascota on d.MascotaID equals m.MascotaID
                       join p in db.Personal on d.EmpleadoID equals p.EmpleadoID
                       join n in db.Estado on d.EstadoID equals n.EstadoID
                       join c in db.Servicio on d.ServicioID equals c.ServicioID

                       select new ListarCitasViewModels
                       {
                           CitaID = d.CitaID,
                           MascotaID = d.MascotaID,
                           EmpleadoID = d.EmpleadoID,
                           FechaCita = d.FechaCita,
                           EstadoID = d.EstadoID,
                           Nombre = m.Nombre, 
                           NombrePersona = p.Nombre + " " + p.Apellido,
                           Descripcion = n.Descripcion,
                           NombreServicio = c.Nombre,

                       }).ToList();
            }

            return View(lts);
        }


    }
}