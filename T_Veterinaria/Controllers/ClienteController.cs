using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_Veterinaria.Models.ViewModels.Editar;
using T_Veterinaria.Models.ViewModels;
using T_Veterinaria.Models;
using T_Veterinaria.Models.New;

namespace T_Veterinaria.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Cliente(string dniFiltro)
        {
            List<ListarClienteViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var query = db.Cliente.AsQueryable();

                if (!string.IsNullOrEmpty(dniFiltro))
                {
                    query = query.Where(c => c.DNI.Contains(dniFiltro)); // Filtro por DNI
                }

                lts = query.Select(d => new ListarClienteViewModels
                {
                    ClienteID = d.ClienteID,
                    NombreCliente = d.NombrePersona,
                    Apellido = d.Apellido,
                    Telefono = d.Telefono,
                    Email = d.Email,
                    Direccion = d.Direccion,
                    DNI = d.DNI
                }).ToList();
            }

            return View(lts);
        }


        public ActionResult NuevoCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoCliente(NuevoCliente model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oCliente = new Cliente();
                        oCliente.ClienteID = model.ClienteID;
                        oCliente.NombrePersona = model.NombreCliente;
                        oCliente.Apellido = model.Apellido;
                        oCliente.Telefono = model.Telefono;
                        oCliente.Email = model.Email;
                        oCliente.Direccion = model.Direccion;
                        oCliente.DNI = model.DNI;

                        db.Cliente.Add(oCliente);
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

        public ActionResult EditarCliente(int? id)
        {
            EditarCliente model = new EditarCliente();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oCliente = db.Cliente.Find(id);
                model.Nombre = oCliente.NombrePersona;
                model.Apellido = oCliente.Apellido;
                model.Telefono = oCliente.Telefono;
                model.Email = oCliente.Email;
                model.Direccion = oCliente.Direccion;
                model.DNI = oCliente.DNI;
                model.ClienteID = oCliente.ClienteID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarCliente(EditarCliente model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oCliente = db.Cliente.Find(model.ClienteID);
                        oCliente.NombrePersona = model.Nombre;
                        oCliente.Apellido = model.Apellido;
                        oCliente.Telefono = model.Telefono;
                        oCliente.Email = model.Email;
                        oCliente.Direccion = model.Direccion;
                        oCliente.DNI = model.DNI;

                        db.Entry(oCliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Cliente/Cliente");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarCliente(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Busca el cliente por su ID
                var cliente = db.Cliente.Find(id);

                if (cliente != null)
                {
                    // Paso 1: Elimina las citas asociadas a las mascotas del cliente
                    var citas = db.Cita.Where(c => c.Mascota.ClienteID == id).ToList();
                    foreach (var cita in citas)
                    {
                        // Elimina las recetas asociadas a la cita
                        var recetas = db.Receta.Where(r => r.Diagnostico.DiagnosticoID == cita.CitaID).ToList();
                        foreach (var receta in recetas)
                        {
                            db.Receta.Remove(receta); // Elimina la receta
                        }

                        // Elimina los diagnósticos relacionados con la cita
                        var diagnosticos = db.Diagnostico.Where(d => d.CitaID == cita.CitaID).ToList();
                        foreach (var diagnostico in diagnosticos)
                        {
                            db.Diagnostico.Remove(diagnostico); // Elimina el diagnóstico
                        }

                        // Elimina el detalle del pago relacionado con la cita
                        var detallePagos = db.Detalle_Pago.Where(dp => dp.CitaID == cita.CitaID).ToList();
                        foreach (var detallePago in detallePagos)
                        {
                            db.Detalle_Pago.Remove(detallePago); // Elimina el detalle de pago
                        }

                        db.Cita.Remove(cita); // Elimina la cita
                    }

                    // Paso 2: Elimina las mascotas asociadas al cliente
                    var mascotas = db.Mascota.Where(m => m.ClienteID == id).ToList();
                    foreach (var mascota in mascotas)
                    {
                        db.Mascota.Remove(mascota); // Elimina la mascota
                    }

                    // Paso 3: Finalmente, elimina el cliente
                    db.Cliente.Remove(cliente);

                    // Guarda todos los cambios en la base de datos
                    db.SaveChanges();
                }
            }

            return Redirect("~/Cliente/Cliente"); // Redirige a la vista que lista a los clientes
        }

        public ActionResult Informacion_Personal(int ClienteID)
        {
            List<ListarClienteViewModels> clienteDetalles;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                clienteDetalles = (from c in db.Cliente
                                   where c.ClienteID == ClienteID
                                   select new ListarClienteViewModels
                                   {
                                       Telefono = c.Telefono,
                                       Email = c.Email,
                                       Direccion = c.Direccion,
                                       DNI = c.DNI,

                                   }).ToList();
            }

            return View(clienteDetalles);
        }
    }
}