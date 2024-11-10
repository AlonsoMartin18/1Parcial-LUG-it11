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
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdDeporte", partido.IdDeporte);
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

        public List<Partido> ObtenerPartidos()
        {
            List<Partido> partidos = new List<Partido>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
            {
                string query = "SELECT * FROM Partido";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Partido partido = new Partido
                    {
                        IdPartido = (int)reader["ID_PARTIDO"],
                        IdDeporte = (int)reader["ID_DEPORTE"],
                        EquipoLocal = reader["EQUIPO_LOCAL"].ToString(),
                        EquipoVisitante = reader["EQUIPO_VISITANTE"].ToString(),
                        FechaRegistro = (DateTime)reader["FECHA_REGISTRO"],
                        FechaPartido = (DateTime)reader["FECHA_PARTIDO"],
                        MarcadorLocal = (int)reader["MARCADOR_LOCAL"],
                        MarcadorVisitante = (int)reader["MARCADOR_VISITANTE"]
                    };
                    partidos.Add(partido);
                }
            }
            return partidos;
        }

        public void EliminarPartido(int idPartido)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
            {
                string query = "DELETE FROM Partido WHERE ID_PARTIDO = @IdPartido";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdPartido", idPartido);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarMarcador(int idPartido, int marcadorLocal, int marcadorVisitante)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
            {
                string query = "UPDATE Partido SET MARCADOR_LOCAL = @MarcadorLocal, MARCADOR_VISITANTE = @MarcadorVisitante WHERE ID_PARTIDO = @IdPartido";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MarcadorLocal", marcadorLocal);
                cmd.Parameters.AddWithValue("@MarcadorVisitante", marcadorVisitante);
                cmd.Parameters.AddWithValue("@IdPartido", idPartido);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Deporte> ObtenerDeportes()
        {
            List<Deporte> deportes = new List<Deporte>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TablaEquiposDB"].ConnectionString))
            {
                string query = "SELECT * FROM Deporte";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Deporte deporte = new Deporte
                    {
                        IdDeporte = (int)reader["ID_DEPORTE"],
                        Descripcion = reader["DESCRIPCION"].ToString()
                    };
                    deportes.Add(deporte);
                }
            }
            return deportes;
        }
    }
}
