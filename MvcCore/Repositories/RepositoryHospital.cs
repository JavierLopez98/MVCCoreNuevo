using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

#region procedures
//----------------------Procedure paginacion singular
//alter procedure PaginarRegistroDepartamento(@Posicion int, @registros int out) 
//as
//select @registros = count(dept_no) from VistaDept
//  select dept_no,dnombre,loc from vistaDept where posicion =@posicion

//go
//---------------------procedure Paginacion por grupos
//create procedure PaginarGrupoDepartamentos(@posicion int, @registros int out)
//as
//select @registros = count(dept_no) from VistaDept
//  select dept_no,dnombre,loc from vistaDept where posicion >=@posicion and posicion<(@posicion+2)
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
        #region Departamentos 
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

        public int GetPaginas(int salto)
        {
            int numeroregistros = this.GetNumeroRegistrosVistaDepartamentos();

            int numeropagina = 1;
            for (int i = 1; i <= numeroregistros; i += salto)
            {
                numeropagina++;
            }
            return numeropagina;
        }

        public VistaDept GetRegistroDepartamento(int posicion)
        {
            return this.context.VistaDepartamentos.Where(x=>x.posicion==posicion).FirstOrDefault();
        }

        public Departamento GetDepartamentoPosicion(int posicion,ref int salida)
        {
            String sql = "PaginarRegistroDepartamento @posicion, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamregistros = new SqlParameter("@registros", -1);
            pamregistros.Direction = System.Data.ParameterDirection.Output;
            Departamento dept = this.context.Departamentos.FromSqlRaw(sql, pampos,pamregistros).AsEnumerable().FirstOrDefault();

            salida = Convert.ToInt32(pamregistros.Value);
            return dept;
        }
        public List<Departamento> GetGrupoDepartamentosProcedure(int posicion,ref int numeroregistros)
        {
            String sql = "PaginarGrupoDepartamentos @posicion , @registros out";
            SqlParameter pamposicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamregistros = new SqlParameter("@registros", -1);
            pamregistros.Direction = System.Data.ParameterDirection.Output;
            List<Departamento> departamentos = this.context.Departamentos.FromSqlRaw(sql, pamposicion, pamregistros).ToList();
            numeroregistros = Convert.ToInt32(pamregistros.Value);

            return departamentos;
        }


        #endregion

        #region Trabajadores

        #region Procedures
        //-------------Procedure Trabajadores con salto
        //create procedure PaginacionTrabajadores(@posicion int, @registros int out, @salto int)
        //as
        //select @registros = count(id) from Trabajadores
        //  select id,apellido,oficio,salario from Trabajadores where posicion>=@posicion and posicion<(@posicion+@salto)
        //go

        //-----------------Procedure Trabajadores Singular
        //        create procedure PaginacionTrabajadoresSingular(@posicion int, @registros int out)
        //as
        //select @registros = count(id) from Trabajadores
        //select id, apellido, oficio, salario from Trabajadores where posicion = @posicion
        //go

        //-----------------Procedure Grupo Salario

        //        create procedure PaginacionTrabajadoresSalario(@posicion int, @registros int out, @salto int, @salario int)
        //as
        //select @registros = count(id) from Trabajadores
        //select id, apellido, oficio, salario from Trabajadores where posicion>=@posicion and posicion<(@posicion+@salto) and salario> @salario
        //go

        //------------Procedure Trabajadores Singular Salario
//        create procedure PaginacionTrabajadoresSingularSalario(@posicion int, @registros int out, @salario int) as
//select @registros = count(id) from Trabajadores
//select id, apellido, oficio, salario from Trabajadores where posicion = @posicion and salario>@salario
//go

        #endregion
        public List<Trabajador> GetGrupoEmpleadosProcedure(int posicion, int salto, ref int numeroregistros)
        {
            
                String Sql = "PaginacionTrabajadores @posicion,@registros out,@salto";
                SqlParameter pampos = new SqlParameter("@posicion", posicion);
                SqlParameter pamreg = new SqlParameter("@registros", -1);
                SqlParameter pamsalto = new SqlParameter("@salto", salto);
                pamreg.Direction = System.Data.ParameterDirection.Output;
                List<Trabajador> trabajadores = this.context.Trabajadors.FromSqlRaw(Sql, pampos, pamreg, pamsalto).ToList();
                numeroregistros = Convert.ToInt32(pamreg.Value);
                return trabajadores;
            
            
        }

        public Trabajador GetTrabajadorPosicion(int posicion, ref int salida)
        {
            String Sql = "PaginacionTrabajadoresSingular @posicion, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            Trabajador trabajador = this.context.Trabajadors.FromSqlRaw(Sql, pampos, pamreg).AsEnumerable().FirstOrDefault();
            salida = Convert.ToInt32(pamreg.Value);
            return trabajador;
        }
        public Trabajador GetTrabajadorPosicionSalario(int posicion, ref int salida,int salario)
        {
            String Sql = "PaginacionTrabajadoresSingular @posicion, @registros out,@salario";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            SqlParameter pamsal = new SqlParameter("@salario", salario);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            Trabajador trabajador = this.context.Trabajadors.FromSqlRaw(Sql, pampos, pamreg,pamsal).AsEnumerable().FirstOrDefault();
            salida = Convert.ToInt32(pamreg.Value);
            return trabajador;
        }

        public List<Trabajador> GetGrupoEmpleadosSalarioProcedure(int posicion, int salto, int salario, ref int numeroregistros)
        {
            String Sql = "PaginacionTrabajadoresSalario @posicion, @registros out, @salto,@salario";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            SqlParameter pamsalto = new SqlParameter("@salto", salto);
            SqlParameter pamsalario = new SqlParameter("@salario", salario);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            List<Trabajador> trabajadores = this.context.Trabajadors.FromSqlRaw(Sql,pampos,pamreg,pamsalto,pamsalario).ToList();
            numeroregistros = Convert.ToInt32(pamreg.Value);
            return trabajadores;
        }

        


        #endregion
    }
}
