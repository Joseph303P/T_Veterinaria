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
    public class ServicioController : Controller
    {
        // GET: Servicio
        public ActionResult Servicio()
        {
            List<ListarServicioViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Servicio
                       select new ListarServicioViewModels
                       {
                           ServicioID = d.ServicioID,
                           NombreServicio = d.Nombre,
                           Descripcion = d.Descripcion,
                           Precio = d.Precio,
                           DuracionMinutos = d.DuracionMinutos,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoServicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoServicio(ListarServicioViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oServicio = new Servicio();
                        oServicio.ServicioID = model.ServicioID;
                        oServicio.Nombre = model.NombreServicio;
                        oServicio.Descripcion = model.Descripcion;
                        oServicio.Precio = model.Precio;
                        oServicio.DuracionMinutos = model.DuracionMinutos;

                        db.Servicio.Add(oServicio);
                        db.SaveChanges();
                    }
                    return Redirect("Servicio");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarServicio(int? id)
        {
            EditarServicio model = new EditarServicio();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oServicio = db.Servicio.Find(id);
                model.ServicioID = oServicio.ServicioID;
                model.NombreServicio = oServicio.Nombre;
                model.Descripcion = oServicio.Descripcion;
                model.Precio = oServicio.Precio;
                model.DuracionMinutos = oServicio.DuracionMinutos;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarServicio(EditarServicio model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oServicio = db.Servicio.Find(model.ServicioID);
                        oServicio.Nombre = model.NombreServicio;
                        oServicio.Descripcion = model.Descripcion;
                        oServicio.Precio = model.Precio;
                        oServicio.DuracionMinutos = model.DuracionMinutos;

                        db.Entry(oServicio).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Servicio/Servicio");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarServicio(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Eliminar las citas asociadas al servicio
                var citasAsociadas = db.Cita.Where(sc => sc.ServicioID == id).ToList();
                if (citasAsociadas.Any())
                {
                    db.Cita.RemoveRange(citasAsociadas);
                }

                // Eliminar el servicio
                var oServicio = db.Servicio.Find(id);
                if (oServicio != null)
                {
                    db.Servicio.Remove(oServicio);
                    db.SaveChanges();
                }
            }

            // Redirigir a la lista de servicios
            return Redirect("~/Servicio/Servicio");
        }

    }
}