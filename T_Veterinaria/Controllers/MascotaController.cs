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
    public class MascotaController : Controller
    {
        // GET: Mascota
        public ActionResult Mascota()
        {
            List<ListarMascotaViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Mascota
                       select new ListarMascotaViewModels
                       {
                           MascotaID = d.MascotaID,
                           ClienteID = d.ClienteID,
                           Nombre = d.Nombre,
                           Especie = d.Especie,
                           Raza = d.Raza,
                           Edad = d.Edad,

                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoMascota()
        {
            List<SelectListItem> clientesDisponibles;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                clientesDisponibles = (from c in db.Cliente
                                       where !db.Mascota.Any(m => m.ClienteID == c.ClienteID) // Filtrar clientes sin mascota
                                       select new SelectListItem
                                       {
                                           Value = c.ClienteID.ToString(),
                                           Text = c.NombrePersona + " " + c.Apellido
                                       }).ToList();
            }

            ViewBag.ClientesDisponibles = clientesDisponibles;
            return View();
        }




        [HttpPost]
        public ActionResult NuevoMascota(NuevaMascota model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oMascota = new Mascota();
                        oMascota.MascotaID = model.MascotaID;
                        oMascota.ClienteID = model.ClienteID;
                        oMascota.Nombre = model.Nombre;
                        oMascota.Especie = model.Especie;
                        oMascota.Raza = model.Raza;
                        oMascota.Edad = model.Edad;

                        db.Mascota.Add(oMascota);
                        db.SaveChanges();
                    }
                    return RedirectToAction("NuevoMascota", "Mascota");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarMascota(int? id)
        {
            EditarMascota model = new EditarMascota();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oMascota = db.Mascota.Find(id);
                model.ClienteID = oMascota.ClienteID;
                model.Nombre = oMascota.Nombre;
                model.Especie = oMascota.Especie;
                model.Raza = oMascota.Raza;
                model.Edad = oMascota.Edad;
                model.MascotaID = oMascota.MascotaID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarMascota(EditarMascota model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oMascota = db.Mascota.Find(model.MascotaID);
                        oMascota.ClienteID = model.ClienteID;
                        oMascota.Nombre = model.Nombre;
                        oMascota.Especie = model.Especie;
                        oMascota.Raza = model.Raza;
                        oMascota.Edad = model.Edad;

                        db.Entry(oMascota).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Mascota/Mascota");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarMascota(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Eliminar citas asociadas a la mascota
                var citas = db.Cita.Where(c => c.MascotaID == id).ToList();
                foreach (var cita in citas)
                {
                    // Eliminar diagnósticos asociados a la cita
                    var diagnosticos = db.Diagnostico.Where(d => d.CitaID == cita.CitaID).ToList();
                    foreach (var diagnostico in diagnosticos)
                    {
                        // Eliminar recetas asociadas al diagnóstico
                        var recetas = db.Receta.Where(r => r.DiagnosticoID == diagnostico.DiagnosticoID).ToList();
                        db.Receta.RemoveRange(recetas);

                        db.Diagnostico.Remove(diagnostico);
                    }

                    // Eliminar detalles de pago asociados a la cita
                    var detallesPago = db.Detalle_Pago.Where(dp => dp.CitaID == cita.CitaID).ToList();
                    db.Detalle_Pago.RemoveRange(detallesPago);

                    db.Cita.Remove(cita);
                }

                // Eliminar la mascota
                var mascota = db.Mascota.Find(id);
                if (mascota != null)
                {
                    db.Mascota.Remove(mascota);
                }

                // Guardar cambios
                db.SaveChanges();
            }

            // Redirigir a la vista de mascotas
            return Redirect("~/Mascota/Mascota");
        }

        public ActionResult Cliente_Mascota()
        {
            List<ListarMascotaViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Mascota
                       join c in db.Cliente on d.ClienteID equals c.ClienteID
                       select new ListarMascotaViewModels
                       {
                           MascotaID = d.MascotaID,
                           Nombre = d.Nombre,
                           Especie = d.Especie,
                           Raza = d.Raza,
                           Edad = d.Edad,
                           NombreCliente = c.NombrePersona,
                       }).ToList();
            }
            return View(lts);
        }
    }
}