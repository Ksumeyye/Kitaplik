using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Kitaplik
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("server=LAPTOP-O1DRK0LF;database=DbKitaplik;integrated security=true;trustServerCertificate=true");
            baglan.Open();
            return baglan;
        }
    }
}
