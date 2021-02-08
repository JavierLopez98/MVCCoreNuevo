using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Interfaces;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryHospital repo;
        public DepartamentosController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            Departamento dept = this.repo.BuscarDepartamento(id);
            return View(dept);
        }

        public IActionResult PaginarVistaDeptRegistro(int? posicion)
        {
            //Si no existe posicion, Mostramos el primer registro
            if (posicion == null)
            {
                posicion = 1;
            }
            int ultimo = this.repo.GetNumeroRegistrosVistaDepartamentos();
            int siguiente = posicion.Value + 1;
            if (siguiente > ultimo)
            {
                siguiente = 1;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = ultimo;
            }
            VistaDept dept = this.repo.GetRegistroDepartamento(posicion.Value);
            ViewData["ultimo"] = ultimo;
            ViewData["Siguiente"] = siguiente;
            ViewData["Anterior"] = anterior;
            ViewData["Posicion"] = posicion.Value;
            return View(dept);
        }

        public IActionResult PaginarVistaDeptGrupo(int? posicion)
        {

            if (posicion == null)
            {
                posicion = 1;
            }
            int numeropagina = 1;
            int numeroregistros = this.repo.GetNumeroRegistrosVistaDepartamentos();

            String html = "<div>";
            for(int i = 1; i <= numeroregistros; i += 2)
            {

                html+="<a href='PaginarVistaDeptGrupo?posicion="+i+"'>"+numeropagina+"</a>";
                numeropagina++;
            }
            html += "</div>";
            ViewData["Paginas"] = html;

            List<VistaDept> depts =this.repo.GetGrupoDepartamento(posicion.Value);
            return View(depts);
        }
    }
}
