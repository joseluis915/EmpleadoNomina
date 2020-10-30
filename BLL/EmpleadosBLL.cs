using System;
using System.Collections.Generic;
//Using agregados
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using EmpleadoNomina.DAL;
using EmpleadoNomina.Entidades;

namespace EmpleadoNomina.BLL
{
    public class EmpleadosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(Empleados empleados)
        {
            if (!Existe(empleados.EmpleadoId))
                return Insertar(empleados);
            else
                return Modificar(empleados);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(Empleados empleados)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Empleados.Add(empleados);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ MODIFICAR ]——————————————————————————————————————————————
        public static bool Modificar(Empleados empleados)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(empleados).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ ELIMINAR ]——————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var empleados = contexto.Empleados.Find(id);
                if (empleados != null)
                {
                    contexto.Empleados.Remove(empleados);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ BUSCAR ]——————————————————————————————————————————————
        public static Empleados Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Empleados empleados;

            try
            {
                empleados = contexto.Empleados.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return empleados;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<Empleados> GetList(Expression<Func<Empleados, bool>> criterio)
        {
            List<Empleados> lista = new List<Empleados>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Empleados.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
        //——————————————————————————————————————————————[ EXISTE ]——————————————————————————————————————————————
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Empleados.Any(c => c.EmpleadoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        //——————————————————————————————————————————————[ GetList ]——————————————————————————————————————————————
        public static List<Empleados> GetList()
        {
            List<Empleados> empleados = new List<Empleados>();
            Contexto contexto = new Contexto();

            try
            {
                empleados = contexto.Empleados.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return empleados;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<Empleados> GetEmpleados()
        {
            List<Empleados> lista = new List<Empleados>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Empleados.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}