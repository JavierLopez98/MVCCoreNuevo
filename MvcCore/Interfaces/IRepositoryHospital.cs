using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Interfaces
{
    public interface IRepositoryHospital
    {
        #region departamento
        List<Departamento> GetDepartamentos();
        Departamento BuscarDepartamento(int id);
        VistaDept GetRegistroDepartamento(int posicion);
        int GetNumeroRegistrosVistaDepartamentos();
        List<VistaDept> GetGrupoDepartamento(int posicion);
        Departamento GetDepartamentoPosicion(int posicion, ref int salida);
        List<Departamento> GetGrupoDepartamentosProcedure(int posicion, ref int numeroregistros);
        #endregion

        int GetPaginas(int salto);

        #region trabajadores
        List<Trabajador> GetGrupoEmpleadosProcedure(int posicion,int salto, ref int numeroregistros);
        Trabajador GetTrabajadorPosicion(int posicion, ref int salida);
        Trabajador GetTrabajadorPosicionSalario(int posicion, ref int salida,int salario);
        List<Trabajador> GetGrupoEmpleadosSalarioProcedure(int posicion, int salto,int salario, ref int numeroregistros);
        #endregion
    }
}
