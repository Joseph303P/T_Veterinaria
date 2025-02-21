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
    public class RecetaController : Controller
    {
        // GET: Receta
        public ActionResult Receta()
        {
            List<ListarRecetaViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Receta
                       select new ListarRecetaViewModels
                       {
                           RecetaID = d.RecetaID,
                           DiagnosticoID = d.DiagnosticoID,
                           TratamientoID = d.TratamientoID,
                           Indicaciones = d.Indicaciones,
                       }).ToList();
            }

            return View(lts);
        }

        public ActionResult NuevaReceta()
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Obtener los diagnósticos que no tienen recetas asociadas
                var DiagnosticosDisponibles = (from d in db.Diagnostico
                                               where !db.Receta.Any(r => r.DiagnosticoID == d.DiagnosticoID)
                                               select new SelectListItem
                                               {
                                                   Value = d.DiagnosticoID.ToString(),
                                                   Text = "Diagnóstico #" + d.DiagnosticoID
                                               }).ToList();

                // Obtener los tratamientos que no tienen recetas asociadas
                var TratamientosDisponibles = (from t in db.Tratamiento
                                               where !db.Receta.Any(r => r.TratamientoID == t.TratamientoID)
                                               select new SelectListItem
                                               {
                                                   Value = t.TratamientoID.ToString(),
                                                   Text = "Tratamiento #" + t.TratamientoID
                                               }).ToList();

                // Pasar los datos a la vista
                ViewBag.DiagnosticosDisponibles = DiagnosticosDisponibles;
                ViewBag.TratamientosDisponibles = TratamientosDisponibles;
            }

            return View();
        }

        [HttpPost]
        public ActionResult NuevaReceta(NuevaReceta model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oReceta = new Receta();
                        oReceta.RecetaID = model.RecetaID;
                        oReceta.DiagnosticoID = model.DiagnosticoID;
                        oReceta.TratamientoID = model.TratamientoID;
                        oReceta.Indicaciones = model.Indicaciones;

                        db.Receta.Add(oReceta);
                        db.SaveChanges();
                    }
                    return Redirect("Receta");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarReceta(int? id)
        {
            EditarReceta model = new EditarReceta();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oReceta = db.Receta.Find(id);
                model.RecetaID = oReceta.RecetaID;
                model.DiagnosticoID = oReceta.DiagnosticoID;
                model.TratamientoID = oReceta.TratamientoID;
                model.Indicaciones = oReceta.Indicaciones;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarReceta(EditarReceta model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oReceta = db.Receta.Find(model.RecetaID);
                        oReceta.DiagnosticoID = model.DiagnosticoID;
                        oReceta.TratamientoID = model.TratamientoID;
                        oReceta.Indicaciones = model.Indicaciones;

                        db.Entry(oReceta).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Receta/Receta");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarReceta(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Buscar la receta por ID
                var receta = db.Receta.Find(id);
                if (receta != null)
                {
                    db.Receta.Remove(receta); // Eliminar la receta
                    db.SaveChanges(); // Guardar los cambios
                }
            }

            // Redirigir a la lista de recetas
            return Redirect("~/Receta/Receta");
        }

        public ActionResult Detalle_Receta()
        {
            List<ListarRecetaViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from r in db.Receta
                       join d in db.Diagnostico on r.DiagnosticoID equals d.DiagnosticoID
                       join t in db.Tratamiento on r.TratamientoID equals t.TratamientoID
                       select new ListarRecetaViewModels
                       {
                           RecetaID = r.RecetaID,
                           Descripcion = d.Descripcion,
                           Detalles = t.Detalles,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult VisualizarIndicaciones(int recetaId)
        {
            List<ListarRecetaViewModels> indicaciones;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                indicaciones = (from r in db.Receta
                                where r.RecetaID == recetaId
                                select new ListarRecetaViewModels
                                {
                                    Indicaciones = r.Indicaciones
                                }).ToList();
            }

            return View(indicaciones);
        }
    }
}
    
