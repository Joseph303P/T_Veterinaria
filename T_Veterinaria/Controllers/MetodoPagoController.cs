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
    public class MetodoPagoController : Controller
    {
        // GET: MetodoPago
        public ActionResult MetodoPago()
        {

            List<Metodo_PagoViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Metodo_Pago
                       select new Metodo_PagoViewModels
                       {
                           MetodoID = d.MetodoID,
                           Nombre = d.Nombre,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoMetodoPago()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoMetodoPago(NuevoMetodoPago model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oMP = new Metodo_Pago();
                        oMP.MetodoID = model.MetodoID;
                        oMP.Nombre = model.Nombre;

                        db.Metodo_Pago.Add(oMP);
                        db.SaveChanges();
                    }
                    return Redirect("MetodoPago");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarMetodoPago(int? id)
        {
            EditarMetodoPago model = new EditarMetodoPago();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oMP = db.Metodo_Pago.Find(id);
                model.Nombre = oMP.Nombre;
                model.MetodoID = oMP.MetodoID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarMetodoPago(EditarMetodoPago model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oMP = db.Metodo_Pago.Find(model.MetodoID);
                        oMP.Nombre = model.Nombre;

                        db.Entry(oMP).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/MetodoPago/MetodoPago");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarMetodoPago(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Buscar pagos asociados al método de pago
                var pagosAsociados = db.Pago.Where(p => p.MetodoID == id).ToList();

                if (pagosAsociados.Any())
                {
                    // Eliminar los pagos asociados
                    foreach (var pago in pagosAsociados)
                    {
                        db.Pago.Remove(pago);
                    }

                    db.SaveChanges(); // Guardar los cambios
                }

                // Buscar el método de pago
                var metodoPago = db.Metodo_Pago.Find(id);
                if (metodoPago != null)
                {
                    db.Metodo_Pago.Remove(metodoPago); // Eliminar el método de pago
                    db.SaveChanges(); // Guardar los cambios
                }
            }

            // Redirigir a la lista de métodos de pago
            return Redirect("~/MetodoPago/MetodoPago");
        }
    }
}