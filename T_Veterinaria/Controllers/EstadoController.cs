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
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Estado()
        {

            List<EstadoViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Estado
                       select new EstadoViewModels
                       {
                           EstadoID = d.EstadoID,
                           Descripcion = d.Descripcion,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoEstado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoEstado(NuevoEstado model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oEstado = new Estado();
                        oEstado.EstadoID = model.EstadoID;
                        oEstado.Descripcion = model.Descripcion;

                        db.Estado.Add(oEstado);
                        db.SaveChanges();
                    }
                    return Redirect("Estado");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarEstado(int? id)
        {
            EditarEstado model = new EditarEstado();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oMP = db.Estado.Find(id);
                model.Descripcion = oMP.Descripcion;
                model.EstadoID = oMP.EstadoID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarEstado(EditarEstado model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oEstado = db.Estado.Find(model.EstadoID);
                        oEstado.Descripcion = model.Descripcion;

                        db.Entry(oEstado).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Estado/Estado");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarEstado(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Verificar si hay citas asociadas al EstadoID
                var citas = db.Cita.Where(c => c.EstadoID == id).ToList();
                if (citas.Any())
                {
                    
                    return Redirect("~/Estado/Estado"); 
                }

                // Buscar el estado por ID
                var estado = db.Estado.Find(id);
                if (estado != null)
                {
                    db.Estado.Remove(estado); 
                    db.SaveChanges(); 
                }
            }

            return Redirect("~/Estado/Estado"); 
        }

    }
}