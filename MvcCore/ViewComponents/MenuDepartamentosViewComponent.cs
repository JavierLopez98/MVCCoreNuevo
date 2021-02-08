using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Interfaces;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.ViewComponents
{
    public class MenuDepartamentosViewComponent:ViewComponent
    {
        //esta clase actua como un controller
        private IRepositoryHospital repo;
        public MenuDepartamentosViewComponent(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //aqui ponemos lo que queramos dibujar
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }
    }
}
