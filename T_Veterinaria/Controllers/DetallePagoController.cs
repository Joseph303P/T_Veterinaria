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
    public class DetallePagoController : Controller
    {
        // GET: DetallePago
        public ActionResult DetallePago()
        {
            List<ListarDetallePago> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Detalle_Pago
                       select new ListarDetallePago
                       {
                           DetalleID = d.DetalleID,
                           PagoID = d.PagoID,
                           CitaID = d.CitaID,
                       }).ToList();
            }

            return View(lts);
        }

        public ActionResult NuevoDetallePago()
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Obtener los pagos que no están asociados con un detalle de pago
                var pagosNoAsociados = db.Pago
                    .Where(p => !db.Detalle_Pago.Any(dp => dp.PagoID == p.PagoID))
                    .Select(p => new SelectListItem
                    {
                        Value = p.PagoID.ToString(),
                        Text = "Pago #" + p.PagoID + " - " + p.Monto // Ejemplo de texto: incluye ID y monto
                    }).ToList();

                // Obtener las citas que no están asociadas con un detalle de pago
                var citasNoAsociadas = db.Cita
                    .Where(c => !db.Detalle_Pago.Any(dp => dp.CitaID == c.CitaID))
                    .Select(c => new SelectListItem
                    {
                        Value = c.CitaID.ToString(),
                        Text = "Cita #" + c.CitaID + " - Fecha: " + c.FechaCita // Ejemplo: incluye ID y fecha
                    }).ToList();

                // Pasar la información a la vista
                ViewBag.PagosNoAsociados = pagosNoAsociados;
                ViewBag.CitasNoAsociadas = citasNoAsociadas;
            }

            return View();
        }


        [HttpPost]
        public ActionResult NuevoDetallePago(NuevoDetallePago model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oDP = new Detalle_Pago();
                        oDP.DetalleID = model.DetalleID;
                        oDP.PagoID = model.PagoID;
                        oDP.CitaID = model.CitaID;

                        db.Detalle_Pago.Add(oDP);
                        db.SaveChanges();
                    }
                    return Redirect("DetallePago");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarDetallePago(int? id)
        {
            EditarDetallePago model = new EditarDetallePago();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oDP = db.Detalle_Pago.Find(id);
                model.PagoID = oDP.PagoID;
                model.CitaID = oDP.CitaID;
                model.DetalleID = oDP.DetalleID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarDetallePago(EditarDetallePago model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oDP = db.Detalle_Pago.Find(model.DetalleID);
                        oDP.PagoID = model.PagoID;
                        oDP.CitaID = model.CitaID;

                        db.Entry(oDP).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/DetallePago/DetallePago");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarDetallePago(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Buscar el detalle de pago por ID
                var detallePago = db.Detalle_Pago.Find(id);
                if (detallePago != null)
                {
                    db.Detalle_Pago.Remove(detallePago); // Eliminar el detalle de pago
                    db.SaveChanges(); // Guardar los cambios
                }
            }

            // Redirigir a la lista de detalles de pago
            return Redirect("~/DetallePago/DetallePago");
        }
    }
}