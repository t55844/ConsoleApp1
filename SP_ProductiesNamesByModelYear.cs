using System;
using Microsoft.Data.SqlClient;


namespace ProductiesNamesByModelYear
{

    public class SP_ProductiesNamesByModelYear : SqlAccess ()
    {
	
	    public void getData(Int16 ModelYear, char SqlFilterByOneYear)
        {
            var SqlConnection con = connectToDataBase();

            try
            {
                con.Open();

                // Create a command
                using ( SqlCommand cmd = new SqlCommand("ProductiesNamesByModelYear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the SqlCommand object
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@MODELYEAR",
                        SqlDbType = SqlDbType.SmallInt,
                        Value = ModelYear
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@BYONEYEAR",
                        SqlDbType = SqlDbType.Char, // Changed to Char for single character
                        Size = 1,
                        Value = SqlFilterByOneYear
                    });

                    // Execute the command and read the results
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (rdr.FieldCount == 2) // Checking if the result set has two columns
                            {
                                Console.WriteLine($"ModelYear: {rdr.GetInt16(0)}, Filter: {rdr.GetString(1)}");
                                Console.WriteLine(rdr.GetValue(2));

                            }
                            else if (!rdr.IsDBNull(0))
                            {
                                Console.WriteLine(rdr.GetString(0));
                            }
                            else
                            {
                                Console.WriteLine("No products to display for the given parameters.");
                            }
                        }
                    }
                }//

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"useStorageProcedure SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" useStorageProcedure  Error: {ex.Message}");
            }

        }
    }

}