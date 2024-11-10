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

            partido.FechaRegistro = DateTime.Now;
            partido.MarcadorLocal = 0;
            partido.MarcadorVisitante = 0;

            partidoData.AgregarPartido(partido);
        }

        public List<Partido> ObtenerPartidos()
        {
            return partidoData.ObtenerPartidos();
        }

        public void EliminarPartido(int idPartido)
        {
            partidoData.EliminarPartido(idPartido);
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

            partidoData.ActualizarMarcador(idPartido, marcadorLocal, marcadorVisitante);
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
