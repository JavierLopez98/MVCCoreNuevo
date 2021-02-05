using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class CochesController : Controller
    {
        private List<Coche> GetCoches()
        {
            
            List<Coche> coches = new List<Coche>();
            Coche car1 = new Coche
            {
                IdCoche=0,
                Marca = "coche",
                Modelo = "cualquiera",
                VelocidadMaxima = 500
            };
            coches.Add(car1);
            Coche car2 = new Coche
            {
                IdCoche = 1,
                Marca = "coche",
                Modelo = "cualquiera",
                VelocidadMaxima = 500
            };
            coches.Add(car2);
            Coche car3 = new Coche
            {
                IdCoche = 2,
                Marca = "coche",
                Modelo = "cualquiera",
                VelocidadMaxima = 500
            };
            coches.Add(car3);
            Coche car4 = new Coche
            {
                IdCoche = 3,
                Marca = "coche",
                Modelo = "cualquiera",
                VelocidadMaxima = 500
            };
            coches.Add(car4);
            Coche car5 = new Coche
            {
                IdCoche = 4,
                Marca = "coche",
                Modelo = "cualquiera",
                VelocidadMaxima = 500
            };
            coches.Add(car5);
            Coche car6 = new Coche
            {
                Marca = "coche",
                Modelo = "cualquiera",
                VelocidadMaxima = 500
            };
            coches.Add(car6);
            return coches;
        }
        public IActionResult Index()
        {
            return View(this.GetCoches());
        }

        public IActionResult CochesAsincronos()
        {
            return View();
        }
        public IActionResult GetCochesPartial()
        {
            List<Coche> coches = this.GetCoches();
            return PartialView("_CochesPartial",coches);
        }

        public IActionResult GetDetallesPartial(int id)
        {
            Coche car = this.GetCoches().SingleOrDefault(z=>z.IdCoche==id);
            return PartialView("_DetallesCochePartial", car);
        }
    }
}
