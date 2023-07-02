using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DisKlinik
{
    internal class ConnectionString
    {

        //SQL BAĞLANTISI 
        public SqlConnection GetCon()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=BATCOMPUTER\SQLEXPRESS;Initial Catalog=DentalDb;Integrated Security=True";
            return baglanti;
        }
    }
}
