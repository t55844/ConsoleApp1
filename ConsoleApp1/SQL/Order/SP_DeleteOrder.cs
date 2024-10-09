using Microsoft.Data.SqlClient;
using SQL;
using System.ComponentModel;
using System.Data;
namespace ConsoleApp1.SQL.Order
{
    internal class SP_DeleteOrder : SqlAccess
    {
        private string procedureName = "DeleteOrder";
        private int ORDER_ID = 0;

        public void setOrderInformation()
        {
            Console.Write("Cancelation of an Order");
            Console.Write("Order Number: ");
            var input = Console.ReadLine();
            ORDER_ID = Convert.ToInt32(input);

        }


        public void deleteData()
        {
            SqlConnection con = connectToDataBase();
            if (ORDER_ID == 0)
            {
                Console.WriteLine("Missing Order Number");
                return;
            }

            try
            {

                con.Open();

                using (SqlCommand cmd = getCmd(procedureName))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Customer Information
                    addSqlParam("@ORDER_ID", SqlDbType.Int, ORDER_ID, cmd);
           
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        if (rdr.FieldCount > 0)
                        {

                            var fieldCount = rdr.FieldCount;
                            for (int i = 0; i < fieldCount; i++)
                            {
                                Console.WriteLine("Order Result: "+rdr.GetValue(i) );
                            }

                        }
                        else
                        {

                            var fieldCount = rdr.FieldCount;
                            for (int i = 0; i < fieldCount; i++)
                            {
                                Console.WriteLine("Tryed read the result but: " + rdr.GetValue(i));

                            }
                        }

                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"useStorageProcedure DeleteOrder SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"useStorageProcedure DeleteOrder Error: {ex.Message}");
            }

            con.Close();
        }

    }
}
