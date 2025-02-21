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
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Pago()
        {
            List<PagoViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Pago
                       select new PagoViewModels
                       {
                           PagoID = d.PagoID,
                           MetodoID = d.MetodoID,
                           FechaPago = d.FechaPago,
                           Monto = d.Monto,
                       }).ToList();
            }

            return View(lts);
        }

        public ActionResult NuevoPago()
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                ViewBag.MetodosDePago = db.Metodo_Pago
                    .Select(m => new SelectListItem
                    {
                        Value = m.MetodoID.ToString(),
                        Text = m.Nombre
                    })
                    .ToList();
            }

            return View();
        }


        [HttpPost]
        public ActionResult NuevoPago(NuevoPago model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oPago = new Pago();
                        oPago.PagoID = model.PagoID;
                        oPago.MetodoID = model.MetodoID;
                        oPago.FechaPago = model.FechaPago;
                        oPago.Monto = model.Monto;

                        db.Pago.Add(oPago);
                        db.SaveChanges();
                    }
                    return RedirectToAction("NuevoDetallePago", "DetallePago");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarPago(int? id)
        {
            EditarPago model = new EditarPago();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oPago = db.Pago.Find(id);
                model.MetodoID = oPago.MetodoID;
                model.FechaPago = oPago.FechaPago;
                model.Monto = oPago.Monto;
                model.PagoID = oPago.PagoID;

                // Pasar los métodos de pago al ViewBag
                ViewBag.MetodosDePago = db.Metodo_Pago
                    .Select(m => new SelectListItem
                    {
                        Value = m.MetodoID.ToString(),
                        Text = m.Nombre
                    })
                    .ToList();
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult EditarPago(EditarPago model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oPago = db.Pago.Find(model.PagoID);
                        oPago.MetodoID = model.MetodoID;
                        oPago.FechaPago = model.FechaPago;
                        oPago.Monto = model.Monto;

                        db.Entry(oPago).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Pago/Pago");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EliminarPago(int id)
        {
            try
            {
                using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                {
                    // Buscar el registro por ID
                    var pago = db.Pago.Find(id);

                    if (pago != null)
                    {
                        db.Pago.Remove(pago); // Eliminar el registro
                        db.SaveChanges(); // Guardar los cambios en la base de datos
                    }
                }

                return RedirectToAction("Pago"); // Redirigir a la lista de pagos
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el pago: " + ex.Message);
            }
        }



        public ActionResult Pago_Metodo()
        {
            List<PagoViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Pago
                       join m in db.Metodo_Pago on d.MetodoID equals m.MetodoID
                       select new PagoViewModels
                       {
                           PagoID = d.PagoID,
                           FechaPago = d.FechaPago,
                           Monto = d.Monto,
                           Nombre = m.Nombre,
                       }).ToList();
            }

            return View(lts);
        }
    }
}