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
    public class CargoController : Controller
    {
        // GET: Cargo
        public ActionResult Cargo()
        {

            List<ListarCargorViewModels> lts;

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                lts = (from d in db.Cargo
                       select new ListarCargorViewModels
                       {
                           CargoId = d.Cargo_Id,
                           Descripcion = d.Descripcion,
                       }).ToList();
            }
            return View(lts);
        }

        public ActionResult NuevoCargo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoCargo(NuevoCargo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oCargo = new Cargo();
                        oCargo.Cargo_Id = model.CargoId;
                        oCargo.Descripcion = model.Descripcion;

                        db.Cargo.Add(oCargo);
                        db.SaveChanges();
                    }
                    return Redirect("Cargo");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ActionResult EditarCargo(int? id)
        {
            EditarCargo model = new EditarCargo();

            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                var oMP = db.Cargo.Find(id);
                model.Descripcion = oMP.Descripcion;
                model.CargoId = oMP.Cargo_Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarCargo(EditarCargo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
                    {
                        var oCargo = db.Cargo.Find(model.CargoId);
                        oCargo.Descripcion = model.Descripcion;

                        db.Entry(oCargo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Cargo/Cargo");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult EliminarCargo(int id)
        {
            using (Veterinaria_BDEntities db = new Veterinaria_BDEntities())
            {
                // Verificar si hay empleados asociados al cargo
                var empleadosAsociados = db.Personal.Where(p => p.Cargo_Id == id).ToList();

                if (empleadosAsociados.Any())
                {
                    // Si hay empleados asociados, eliminarlos o reasignarlos según las reglas de negocio
                    foreach (var empleado in empleadosAsociados)
                    {
                        empleado.Cargo_Id = null; // Reasignar el cargo a null (si está permitido)
                                                  // Alternativamente, eliminar empleados: db.Personal.Remove(empleado);
                    }

                    db.SaveChanges(); // Guardar cambios después de la reasignación o eliminación
                }

                // Buscar el cargo por ID
                var cargo = db.Cargo.Find(id);
                if (cargo != null)
                {
                    db.Cargo.Remove(cargo); // Eliminar el cargo
                    db.SaveChanges(); // Guardar los cambios
                }
            }

            // Redirigir a la lista de cargos
            return Redirect("~/Cargo/Cargo");
        }


    }
}
    
