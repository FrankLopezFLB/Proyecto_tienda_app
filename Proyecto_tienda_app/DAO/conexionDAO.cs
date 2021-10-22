using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_tienda_app.DAO
{
    public class conexionDAO
    {
        SqlConnection cn = new SqlConnection(

        ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        public SqlConnection getcn

        {

            get { return cn; }

        }
    }
}