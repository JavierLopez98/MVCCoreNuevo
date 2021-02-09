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

        public IActionResult PaginarVistaDeptGrupo(int? pagina)
        {
            int salto = 2;
            if (pagina == null)
            {
                pagina = 1;
            }
            int posicion = salto * pagina.Value - 1;
            //int numeropagina = 1;
            //int numeroregistros = this.repo.GetNumeroRegistrosVistaDepartamentos();

            //String html = "<div>";
            //for(int i = 1; i <= numeroregistros; i += 2)
            //{

            //    html+="<a href='PaginarVistaDeptGrupo?posicion="+i+"'>"+numeropagina+"</a>";
            //    numeropagina++;
            //}
            //html += "</div>";
            //ViewData["Paginas"] = html;

            ViewData["Paginas"] = this.repo.GetPaginas(salto);

            List<VistaDept> depts =this.repo.GetGrupoDepartamento(posicion);
            return View(depts);
        }

        public IActionResult PaginarRegistroProcedure(int? posicion)
        {

            if (posicion == null)
            {
                posicion = 1;
            }
            int ultimo = 0;
            Departamento dept = this.repo.GetDepartamentoPosicion(posicion.Value,ref ultimo);
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
            ViewData["ultimo"] = ultimo;
            ViewData["Siguiente"] = siguiente;
            ViewData["Anterior"] = anterior;
            ViewData["Posicion"] = posicion.Value;
            return View(dept);
        }

        public IActionResult PaginacionGruposProcedure(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroRegistros = 0;
            List<Departamento> depts = this.repo.GetGrupoDepartamentosProcedure(posicion.Value,ref numeroRegistros);
            ViewData["registros"] = numeroRegistros;
            return View(depts);
        }
    }
}
