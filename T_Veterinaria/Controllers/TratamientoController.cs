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
    public class TratamientoController : Controller
    {
        // GET: Tratamiento
        public ActionResult Tratamiento()
        {

            List<TratamientoViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Tratamiento
                       select new TratamientoViewModels
                       {
                           TratamientoID = d.TratamientoID,
                           Detalles = d.Detalles,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoTratamiento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoTratamiento(NuevoTratamiento model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oTratamiento = new Tratamiento();
                        oTratamiento.TratamientoID = model.TratamientoID;
                        oTratamiento.Detalles = model.Detalles;

                        db.Tratamiento.Add(oTratamiento);
                        db.SaveChanges();
                    }
                    return RedirectToAction("NuevaReceta", "Receta");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarTratamiento(int? id)
        {
            EditarTratamiento model = new EditarTratamiento();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oTratamiento = db.Tratamiento.Find(id);
                model.Detalles = oTratamiento.Detalles;
                model.TratamientoID = oTratamiento.TratamientoID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarTratamiento(EditarTratamiento model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oTratamiento = db.Tratamiento.Find(model.TratamientoID);
                        oTratamiento.Detalles = model.Detalles;

                        db.Entry(oTratamiento).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Tratamiento/Tratamiento");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarTratamiento(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var ElimininarReceta = db.Receta.Where(d => d.TratamientoID == id).ToList();
                db.Receta.RemoveRange(ElimininarReceta);

                var oTratamiento = db.Tratamiento.Find(id);
                if (oTratamiento != null)
                {
                    db.Tratamiento.Remove(oTratamiento);
                    db.SaveChanges();
                }
            }

            return Redirect("~/Tratamiento/Tratamiento"); // Redirige a la vista que lista a los clientes
        }

        public ActionResult VisualizarTrataimiento(int TratamientoID)
        {
            List<TratamientoViewModels> Tratamiento;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                Tratamiento = (from d in db.Tratamiento
                               where d.TratamientoID == TratamientoID
                               select new TratamientoViewModels
                               {
                                  Detalles  = d.Detalles
                               }).ToList();
            }

            return View(Tratamiento);
        }
    }
}