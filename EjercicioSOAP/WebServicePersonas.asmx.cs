using EjercicioSOAP.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Services;

namespace EjercicioSOAP
{
    /// <summary>
    /// Descripción breve de WebServicePersonas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePersonas : System.Web.Services.WebService
    {
        PersonasEntities db = new PersonasEntities();

        [WebMethod]
        public Personas[] listarPersonas()
        {
            var personas = db.Personas.ToArray();

            return personas;
        }

        [WebMethod]
        public Personas verPersona(int id)
        {
            var persona = db.Personas.Where(x=>x.PersonaId==id).First();

            return persona;
        }

        [WebMethod]
        public string insertarPersona(Personas persona)
        {
            string respuesta = null;
            try
            {
                db.Personas.Add(persona);
                db.Entry(persona).State = EntityState.Added;
                db.SaveChanges();

            } catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            respuesta = "Persona insertada con éxito!";

            return respuesta;
        }

        [WebMethod]
        public string modificarPersona(Personas persona)
        {
            string respuesta = null;
            Personas modificar;
            try
            {
                modificar = db.Personas.Where(x=>x.PersonaId==persona.PersonaId).First();
                modificar.Nombre = persona.Nombre;
                modificar.Apellidos = persona.Apellidos;
                modificar.EstadoCivil = persona.EstadoCivil;
                modificar.Sexo = persona.Sexo;
                modificar.CodigoPostal = persona.CodigoPostal;
                modificar.Direccion = persona.Direccion;
                modificar.Provincia = persona.Provincia;

                
                db.SaveChanges();

                db.Entry(persona).State = EntityState.Modified;

            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            respuesta = "Persona modificada con éxito!";

            return respuesta;
        }

        [WebMethod]
        public string elimiarPersona(int id)
        {
            string respuesta = null;
            try
            {
                var eliminar = db.Personas.Where(x => x.PersonaId == id).First();
                db.Personas.Remove(eliminar);

                db.Entry(eliminar).State = EntityState.Deleted;
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            respuesta = "Persona eliminada con éxito!";

            return respuesta;
        }
    }
}
