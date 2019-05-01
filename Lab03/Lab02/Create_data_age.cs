using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    public class AgeCount
    {
        public int Age { get; set; }
        public int Count { get; set; }
    }

    public class Create_data_age
    {
        static public List<AgeCount> ageData = new List<AgeCount>();

        public Create_data_age(PersonContext context)
        {

            try
            {
                SqlConnection connection = context.Database.Connection as SqlConnection;
                connection.Open();
                SqlCommand komendaSQL = connection.CreateCommand();
                komendaSQL.CommandText = "SELECT * FROM dbo.People";
                SqlDataReader thisReader = komendaSQL.ExecuteReader();

                Console.WriteLine("Wiersze tabeli:");
                while (thisReader.Read())
                {
                    if (ageData != null)
                    {
                        if (ageData.Any(info =>
                           info.Age == int.Parse(thisReader["Age"].ToString())
                          ))
                        {
                            AgeCount tmp = ageData.Where(t => t.Age == int.Parse(thisReader["Age"].ToString())).First();
                            tmp.Count += 1;
                        }
                        else
                            ageData.Add(new AgeCount { Age = int.Parse(thisReader["Age"].ToString()), Count = 1 });
                    }
                    else
                        ageData.Add(new AgeCount { Age = int.Parse(thisReader["Age"].ToString()), Count = 1 });
                    Console.WriteLine(thisReader["Age"].ToString() + "   " + thisReader["Name"].ToString());
                }

                thisReader.Close();
                connection.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}

