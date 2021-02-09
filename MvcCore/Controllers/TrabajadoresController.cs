using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MvcCoreNuevo.Interfaces;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class TrabajadoresController : Controller
    {
        private IMemoryCache memory;
        private IRepositoryHospital repo;
        public TrabajadoresController(IRepositoryHospital repo,IMemoryCache memory)
        {
            this.repo = repo;
            this.memory = memory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PaginacionSimple(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int ultimo = 0;
            Trabajador trabajador = this.repo.GetTrabajadorPosicion(posicion.Value, ref ultimo);
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
            return View(trabajador);
        }

        

        public IActionResult PaginacionGrupo(int?pagina,int?salario,int?salto)
        {
            if (salto == null)
            {
                salto = 4;
            }
            if (salario == null)
            {
                salario = 0;
            }
            if (pagina == null)
            {
                pagina = 1;
            }
            int posicion = pagina.Value * salto.Value - (salto.Value - 1);
            int numeroRegistros = 0;
            //List<Trabajador> trabajadores = this.repo.GetGrupoEmpleadosProcedure(posicion,salto.Value,ref numeroRegistros);
            List<Trabajador> trabajadores = this.repo.GetGrupoEmpleadosSalarioProcedure(posicion, salto.Value, salario.Value, ref numeroRegistros);
            ViewData["paginas"] = numeroRegistros;
            ViewData["salto"] = salto;
            ViewData["salario"] = salario;
            return View(trabajadores);
        }
        //[HttpPost]
        //public IActionResult PaginacionGrupo(int? posicion,int? salto,int? salario)
        //{
        //    if (posicion == null)
        //    {
        //        posicion = 1;
        //    }
        //    if (salto == null)
        //    {
        //        salto = 4;
        //    }
        //    if (salario == null)
        //    {
        //        salario = 0;
        //    }
        //    int numeroRegistros = 0;
        //    //List<Trabajador> trabajadores = this.repo.GetGrupoEmpleadosProcedure(posicion.Value, salto.Value, ref numeroRegistros);
        //    List<Trabajador> trabajadores = this.repo.GetGrupoEmpleadosSalarioProcedure(posicion.Value, salto.Value, salario.Value, ref numeroRegistros);
        //    ViewData["paginas"] = numeroRegistros;
        //    ViewData["salto"] = salto;
        //    ViewData["salario"] = salario;
        //    return View(trabajadores);
        //}
    }
}
