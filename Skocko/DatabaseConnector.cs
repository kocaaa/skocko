using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Skocko
{
    public sealed class DatabaseConnector
    {

        private static readonly object key = new object();

        public SqlConnection connection;

        private DatabaseConnector()
        {
            string connectString = @"Data Source=(localdb)\database;Initial Catalog=skocko;Integrated Security=True";
            connection = new SqlConnection(connectString);
        }

        private static DatabaseConnector instance = null;

        public static DatabaseConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                        if (instance == null)
                            instance = new DatabaseConnector();
                }

                return instance;
            }

            set { }
        }





    }
}
