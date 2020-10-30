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
    public class NominasBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(Nominas nominas)
        {
            if (!Existe(nominas.NominaId))
                return Insertar(nominas);
            else
                return Modificar(nominas);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(Nominas nominas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Nominas.Add(nominas);
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
        public static bool Modificar(Nominas nominas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(nominas).State = EntityState.Modified;
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
                var nominas = contexto.Nominas.Find(id);
                if (nominas != null)
                {
                    contexto.Nominas.Remove(nominas);
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
        public static Nominas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Nominas nominas;

            try
            {
                nominas = contexto.Nominas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return nominas;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<Nominas> GetList(Expression<Func<Nominas, bool>> criterio)
        {
            List<Nominas> lista = new List<Nominas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Nominas.Where(criterio).ToList();
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
                encontrado = contexto.Nominas.Any(c => c.NominaId == id);
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
        public static List<Nominas> GetList()
        {
            List<Nominas> nominas = new List<Nominas>();
            Contexto contexto = new Contexto();

            try
            {
                nominas = contexto.Nominas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return nominas;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<Nominas> GetNominas()
        {
            List<Nominas> lista = new List<Nominas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Nominas.ToList();
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