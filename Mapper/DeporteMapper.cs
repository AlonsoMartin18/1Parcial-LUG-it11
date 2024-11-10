using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Mapper
{
    public class DeporteMapper
    {
        public static Deporte Map(SqlDataReader reader)
        {
            return new Deporte
            {
                IdDeporte = Convert.ToInt32(reader["ID_DEPORTE"]),
                Descripcion = reader["DESCRIPCION"].ToString()
            };
        }
    }
}
