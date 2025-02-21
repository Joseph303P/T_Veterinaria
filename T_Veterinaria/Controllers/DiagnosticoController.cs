using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_Veterinaria.Models.New;
using T_Veterinaria.Models.ViewModels.Editar;
using T_Veterinaria.Models.ViewModels;
using T_Veterinaria.Models;

namespace T_Veterinaria.Controllers
{
    public class DiagnosticoController : Controller
    {
        // GET: Diagnostico
        public ActionResult Diagnostico()
        {
            List<DiagnosticoViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Diagnostico
                       select new DiagnosticoViewModels
                       {
                           DiagnosticoID = d.DiagnosticoID,
                           CitaID = d.CitaID,
                           Descripcion = d.Descripcion,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoDiagnostico()
        {
            List<SelectListItem> CitasDisponibles;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Obtener las citas que no están asociadas con ningún diagnóstico
                CitasDisponibles = (from c in db.Cita
                                    where !db.Diagnostico.Any(d => d.CitaID == c.CitaID) // Aquí verificas en la tabla Diagnostico
                                    select new SelectListItem
                                    {
                                        Value = c.CitaID.ToString(),
                                        Text = "Cita #" + c.CitaID + " - " + c.FechaCita // Personaliza el texto si es necesario
                                    }).ToList();

                // Asignar las citas disponibles al ViewBag para usar en la vista
                ViewBag.CitaPendiente = CitasDisponibles;
            }

            return View();
        }


        [HttpPost]
        public ActionResult NuevoDiagnostico(NuevoDiagnostico model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oDiagnostico = new Diagnostico();
                        oDiagnostico.DiagnosticoID = model.DiagnosticoID;
                        oDiagnostico.CitaID = model.CitaID;
                        oDiagnostico.Descripcion = model.Descripcion;

                        db.Diagnostico.Add(oDiagnostico);
                        db.SaveChanges();
                    }
                    return RedirectToAction("NuevoTratamiento", "Tratamiento");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarDiagnostico(int? id)
        {
            EditarDiagnostico model = new EditarDiagnostico();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oDiagnostico = db.Diagnostico.Find(id);
                model.CitaID = oDiagnostico.CitaID;
                model.Descripcion = oDiagnostico.Descripcion;
                model.DiagnosticoID = oDiagnostico.DiagnosticoID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarDiagnostico(EditarDiagnostico model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oDiagnostico = db.Diagnostico.Find(model.DiagnosticoID);
                        oDiagnostico.CitaID = model.CitaID;
                        oDiagnostico.Descripcion = model.Descripcion;

                        db.Entry(oDiagnostico).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Diagnostico/Diagnostico");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarDiagnostico(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Validar si existe el diagnóstico
                var diagnostico = db.Diagnostico.Find(id);
                if (diagnostico == null)
                {
                    TempData["Error"] = "El diagnóstico no existe.";
                    return Redirect("~/Diagnostico/Diagnostico");
                }

                // Validar si el diagnóstico tiene recetas asociadas
                bool tieneRecetas = db.Receta.Any(r => r.DiagnosticoID == id);
                if (tieneRecetas)
                {
                    TempData["Error"] = "No se puede eliminar un diagnóstico con recetas asociadas.";
                    return Redirect("~/Diagnostico/Diagnostico");
                }

                // Eliminar recetas asociadas (si decides hacerlo automáticamente)
                var recetas = db.Receta.Where(r => r.DiagnosticoID == id).ToList();
                db.Receta.RemoveRange(recetas);

                // Eliminar el diagnóstico
                db.Diagnostico.Remove(diagnostico);

                // Guardar cambios
                db.SaveChanges();
            }

            return Redirect("~/Diagnostico/Diagnostico");
        }

        public ActionResult VisualizarDiagnostico(int DiagnosticoID)
        {
            List<DiagnosticoViewModels> Diagnostico;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                Diagnostico = (from d in db.Diagnostico
                                where d.DiagnosticoID ==DiagnosticoID
                                select new DiagnosticoViewModels
                                {
                                    Descripcion = d.Descripcion
                                }).ToList();
            }

            return View(Diagnostico);
        }
    }
}