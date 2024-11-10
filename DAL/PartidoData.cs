using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class PartidoData
    {        

        public void AgregarPartido(Partido partido)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
            {
                string query = "INSERT INTO Partido (ID_DEPORTE, EQUIPO_LOCAL, EQUIPO_VISITANTE, FECHA_REGISTRO, FECHA_PARTIDO, MARCADOR_LOCAL, MARCADOR_VISITANTE) " +
                               "VALUES (@IdDeporte, @EquipoLocal, @EquipoVisitante, @FechaRegistro, @FechaPartido, @MarcadorLocal, @MarcadorVisitante)";
                try
                {
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdDeporte", partido.Deporte.IdDeporte);
                        cmd.Parameters.AddWithValue("@EquipoLocal", partido.EquipoLocal);
                        cmd.Parameters.AddWithValue("@EquipoVisitante", partido.EquipoVisitante);
                        cmd.Parameters.AddWithValue("@FechaRegistro", partido.FechaRegistro);
                        cmd.Parameters.AddWithValue("@FechaPartido", partido.FechaPartido);
                        cmd.Parameters.AddWithValue("@MarcadorLocal", partido.MarcadorLocal);
                        cmd.Parameters.AddWithValue("@MarcadorVisitante", partido.MarcadorVisitante);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al agregar el partido: " + ex.Message);
                }
            }
        }

        public List<Partido> ObtenerPartidos()
        {
            List<Partido> partidos = new List<Partido>();
            string query = "SELECT * FROM Partido";

            try
            {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
                    {
                         using(SqlCommand cmd = new SqlCommand(query, connection))
                         {
                            connection.Open();
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {   
                                while (reader.Read())
                                {                               
                                     int idDeporte = (int)reader["ID_DEPORTE"];
                                     Deporte deporte = ObtenerDeporte(idDeporte);
                                     Partido partido = PartidoMapper.Map(reader, deporte);
                                     partidos.Add(partido);                                   
                                }
                            }
                         }      
                         return partidos;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error al obtener los partidos: " + ex.Message);
                    }
            }
        }
            

        public void EliminarPartido(int idPartido)
        {
            string query = "DELETE FROM Partido WHERE ID_PARTIDO = @IdPartido";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
                {               
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdPartido", idPartido);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el partido: " + ex.Message);
            }
        }

        public void ActualizarMarcador(int idPartido, int marcadorLocal, int marcadorVisitante)
        {
            string query = "UPDATE Partido SET MARCADOR_LOCAL = @MarcadorLocal, MARCADOR_VISITANTE = @MarcadorVisitante WHERE ID_PARTIDO = @IdPartido";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
                {                    
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MarcadorLocal", marcadorLocal);
                        cmd.Parameters.AddWithValue("@MarcadorVisitante", marcadorVisitante);
                        cmd.Parameters.AddWithValue("@IdPartido", idPartido);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el marcador: " + ex.Message);
            }
        }

        public List<Deporte> ObtenerDeportes()
        {
            List<Deporte> deportes = new List<Deporte>();
            string query = "SELECT * FROM Deporte";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
                {                    
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                 deportes.Add(DeporteMapper.Map(reader));
                            }
                        }
                    }
                }
                return deportes;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los deportes: " + ex.Message);
            }
        }
    }
}

