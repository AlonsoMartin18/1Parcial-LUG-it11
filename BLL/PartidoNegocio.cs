using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class PartidoNegocio
    {
        private PartidoData partidoData = new PartidoData();

        public void CrearPartido(Partido partido)
        {
            if (string.IsNullOrWhiteSpace(partido.EquipoLocal) || partido.EquipoLocal.Length <= 5 ||
                string.IsNullOrWhiteSpace(partido.EquipoVisitante) || partido.EquipoVisitante.Length <= 5)
            {
                throw new ArgumentException("El equipo local y visitante no pueden estar vacíos y deben tener más de 5 caracteres.");
            }

            if (partido.FechaPartido < DateTime.Now)
            {
                throw new ArgumentException("La fecha del partido no puede ser menor a la fecha actual.");
            }

            using(TransactionScope scope = new TransactionScope())
            {
                try
                {
                    partidoData.AgregarPartido(partido);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al agregar el partido: " + ex.Message);
                }               
            }
        }

        public List<Partido> ObtenerPartidos()
        {
            try
            {
                return partidoData.ObtenerPartidos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los partidos: " + ex.Message);
            }
        }

        public void EliminarPartido(int idPartido)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    partidoData.EliminarPartido(idPartido);
                    scope.Complete();
                }            
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el partido: " + ex.Message);
                }
            }
        }

        public void ActualizarMarcador(int idPartido, int marcadorLocal, int marcadorVisitante)
        {
            Partido partido = partidoData.ObtenerPartidos().Find(p => p.IdPartido == idPartido);
            if (partido == null || partido.FechaPartido.Date <= DateTime.Now.Date)
            {
                throw new ArgumentException("Solo se puede modificar el marcador de partidos que se jueguen el día de hoy.");
            }

            if (marcadorLocal < 0 || marcadorVisitante < 0)
            {
                throw new ArgumentException("El marcador no puede ser menor a cero.");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    partidoData.ActualizarMarcador(idPartido, marcadorLocal, marcadorVisitante);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar el marcador" + ex.Message);
                }
            }               
        }

        public List<Deporte> ObtenerDeportes()
        {
            try
            {
                return partidoData.ObtenerDeportes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los deportes: " + ex.Message);
            }
        }
    }
}
