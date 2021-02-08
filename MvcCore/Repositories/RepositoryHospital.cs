using MvcCoreNuevo.Data;
using MvcCoreNuevo.Interfaces;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



#region VistaDepartamentosPaginacion

//create view VistaDept as
//select IsNull(ROW_NUMBER() over(order by dept_no),0)as posicion,
//IsNull(dept.DEPT_NO, 0) as dept_no,Dept.DNOMBRE,DEPT.LOC from Dept
//go
#endregion


namespace MvcCoreNuevo.Repositories
{
    
    
    public class RepositoryHospital : IRepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }
        public Departamento BuscarDepartamento(int id)
        {
            return this.context.Departamentos.SingleOrDefault(x => x.Numero == id);
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public List<VistaDept> GetGrupoDepartamento(int posicion)
        {
            //select* from VistaDept where posicion >= 1 and Posicion<(1 + 2)
            var consulta= from datos in this.context.VistaDepartamentos where datos.posicion>=posicion && datos.posicion<(posicion+2) select datos;
            return consulta.ToList();
        }

        public int GetNumeroRegistrosVistaDepartamentos()
        {
            return this.context.VistaDepartamentos.Count();
        }

        public VistaDept GetRegistroDepartamento(int posicion)
        {
            return this.context.VistaDepartamentos.Where(x=>x.posicion==posicion).FirstOrDefault();
        }
    }
}
