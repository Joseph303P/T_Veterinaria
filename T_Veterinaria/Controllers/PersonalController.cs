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
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Personal()
        {
            List<ListarPersonalViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Personal
                       select new ListarPersonalViewModels
                       {
                           EmpleadoID = d.EmpleadoID,
                           CargoId = d.Cargo_Id,
                           NombrePersona = d.Nombre,
                           Apellido = d.Apellido,
                           Telefono = d.Telefono,
                           Email = d.Email,
                           Direccion = d.Direccion,
                           DNI = d.DNI,
                       }).ToList();
            }

            return View(lts);
        }

        public ActionResult NuevoPersonal()
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                ViewBag.Cargos = db.Cargo
                    .Select(m => new SelectListItem
                    {
                        Value = m.Cargo_Id.ToString(),
                        Text = m.Descripcion
                    })
                    .ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult NuevoPersonal(NuevoPersonal model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oPersonal = new Personal();
                        oPersonal.EmpleadoID = model.EmpleadoID;
                        oPersonal.Cargo_Id = model.CargoId;
                        oPersonal.Nombre = model.Nombre;
                        oPersonal.Apellido = model.Apellido;
                        oPersonal.Telefono = model.Telefono;
                        oPersonal.Email = model.Email;
                        oPersonal.Direccion = model.Direccion;
                        oPersonal.DNI = model.DNI;

                        db.Personal.Add(oPersonal);
                        db.SaveChanges();
                    }
                    return Redirect("Personal");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarPersonal(int? id)
        {
            EditarPersonal model = new EditarPersonal();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oPersonal = db.Personal.Find(id);
                model.Nombre = oPersonal.Nombre;
                model.CargoId = oPersonal.Cargo_Id;
                model.Apellido = oPersonal.Apellido;
                model.Telefono = oPersonal.Telefono;
                model.Email = oPersonal.Email;
                model.Direccion = oPersonal.Direccion;
                model.DNI = oPersonal.DNI;
                model.EmpleadoID = oPersonal.EmpleadoID;
                model.CargoId = oPersonal.Cargo_Id;

                ViewBag.Cargos = db.Cargo
            .Select(m => new SelectListItem
            {
                Value = m.Cargo_Id.ToString(),
                Text = m.Descripcion
            }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarPersonal(EditarPersonal model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oPersonal = db.Personal.Find(model.EmpleadoID);

                        oPersonal.Cargo_Id = model.CargoId;
                        oPersonal.Nombre = model.Nombre;
                        oPersonal.Apellido = model.Apellido;
                        oPersonal.Telefono = model.Telefono;
                        oPersonal.Email = model.Email;
                        oPersonal.Direccion = model.Direccion;
                        oPersonal.DNI = model.DNI;

                        db.Entry(oPersonal).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Personal/Personal");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarPersonal(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Eliminar todas las citas asociadas al empleado
                var EliminarCita = db.Cita.Where(d => d.EmpleadoID == id).ToList();
                db.Cita.RemoveRange(EliminarCita);

                // Buscar al empleado
                var oPersonal = db.Personal.Find(id);
                if (oPersonal != null)
                {
                    db.Personal.Remove(oPersonal); // Eliminar al empleado
                    db.SaveChanges(); // Guardar los cambios
                }
            }
            return Redirect("~/Personal/Personal"); // Redirigir a la lista
        }


        public ActionResult Cargo_Personal()
        {
            List<ListarPersonalViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Personal
                       join c in db.Cargo on d.Cargo_Id equals c.Cargo_Id
                       select new ListarPersonalViewModels
                       {
                           Descripcion = c.Descripcion,
                           NombrePersona = d.Nombre,
                           Apellido = d.Apellido,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult Inf_Personal_Empleado(int EmpleadoID)
        {
            List<ListarPersonalViewModels> personaleDetalles;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                personaleDetalles = (from c in db.Personal
                                   where c.EmpleadoID == EmpleadoID
                                     select new ListarPersonalViewModels
                                     {
                                       Telefono = c.Telefono,
                                       Email = c.Email,
                                       Direccion = c.Direccion,
                                       DNI = c.DNI,
                                     }).ToList();
            }
            return View(personaleDetalles);
        }
        
    }
}