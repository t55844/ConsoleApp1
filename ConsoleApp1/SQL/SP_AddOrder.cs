using Microsoft.Data.SqlClient;
using SQL;
using System.Data;
using System.Reflection.PortableExecutable;

/*
USE BikeStores

SELECT * FROM ORDERS

SELECT * FROM ORDER_ITEMS WHERE discount = 0.15

delete from ORDERS WHERE order_date = '2024-09-12'
delete from ORDER_ITEMS WHERE discount = 0.15

EXECUTE dbo.AddOrder @CUS_FIR_NAME = 'Debra',
@CUS_LAS_NAME = 'Burks',
@STORE_NAME = 'Santa Cruz Bikes',
@STAF_FIR_NAME = 'Fabiola',
@STAF_LAS_NAME = 'Jackson',

--PRE ORTDER DATA
@ORDER_STATUS = 4,
@ORDER_DATE = '2024-09-12',
@REQUIRED_DATE = '2024-09-17',
@SHIPPED_DATE = '2024-09-16',

--PRE ITEM ORDER
@PRODUCT_NAME = 'Trek 820 - 2016',
@DISCOUNT = 0.15,
@QUANTITY = 1 */

namespace AddOrder
{

    public class SP_AddOrder : SqlAccess
    {
        private string procedureName = "AddOrder";

        public SP_AddOrder()
        {
        }

        public class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public short OrderStatus { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime? RequiredDate { get; set; }
            public DateTime? ShippedDate { get; set; }
            public int StoreId { get; set; }
            public int StaffId { get; set; }
            public int ItemId { get; set; }
            public int ProductId { get; set; }
            public short Quantity { get; set; }
            public float ListPrice { get; set; }
            public float Discount { get; set; }
        }

        private Order ReadOrder(SqlDataReader reader)
        {
            /*
                              O.order_id, int32
                               O.customer_id, int32
                               O.order_status, int16
                               O.order_date, data
                               O.required_date, date
                               O.shipped_date, date
                               O.store_id, int32
                               O.staff_id, int32
                               OT.item_id, int32
                               OT.product_id, int32
                               OT.quantity, int16
                               OT.list_price, float
                               OT.discount, float 
                            */
            var order = new Order
            {
                OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                CustomerId = reader.GetInt32(reader.GetOrdinal("customer_id")),
                OrderStatus = reader.GetInt16(reader.GetOrdinal("order_status")),
                OrderDate = reader.GetDateTime(reader.GetOrdinal("order_date")),
                RequiredDate = reader.IsDBNull(reader.GetOrdinal("required_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("required_date")),
                ShippedDate = reader.IsDBNull(reader.GetOrdinal("shipped_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("shipped_date")),
                StoreId = reader.GetInt32(reader.GetOrdinal("store_id")),
                StaffId = reader.GetInt32(reader.GetOrdinal("staff_id")),
                ItemId = reader.GetInt32(reader.GetOrdinal("item_id")),
                ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                ListPrice = reader.GetFloat(reader.GetOrdinal("list_price")),
                Discount = reader.GetFloat(reader.GetOrdinal("discount"))
            };

           

            return order;
        }

        public void getData()
        {
            SqlConnection con = connectToDataBase();

            try
            {

                con.Open();
                
                using (SqlCommand cmd = getCmd(procedureName))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Customer Information
                    addSqlParam("@CUS_FIR_NAME", SqlDbType.NVarChar, "Debra", cmd);
                    addSqlParam("@CUS_LAS_NAME", SqlDbType.NVarChar, "Burks", cmd);
                    addSqlParam("@STORE_NAME", SqlDbType.NVarChar, "Santa Cruz Bikes", cmd);
                    addSqlParam("@STAF_FIR_NAME", SqlDbType.NVarChar, "Fabiola", cmd);
                    addSqlParam("@STAF_LAS_NAME", SqlDbType.NVarChar, "Jackson", cmd);

                    // Order Data
                    addSqlParam("@ORDER_STATUS", SqlDbType.Int, 4, cmd);
                    addSqlParam("@ORDER_DATE", SqlDbType.Date, "2024-9-12", cmd);
                    addSqlParam("@REQUIRED_DATE", SqlDbType.Date, "2024-9-17", cmd);
                    addSqlParam("@SHIPPED_DATE", SqlDbType.Date, "2024-9-16", cmd);

                    // Item Order
                    addSqlParam("@PRODUCT_NAME", SqlDbType.NVarChar, "Trek 820 - 2016", cmd);
                    addSqlParam("@DISCOUNT", SqlDbType.Float, 0.15, cmd);
                    addSqlParam("@QUANTITY", SqlDbType.Int, 1, cmd);


                    SqlDataReader rdr = cmd.ExecuteReader();
                    
                    Console.WriteLine("erro pre loop read");
                    try { 
                    Order order = ReadOrder(rdr);
                        Console.WriteLine("order received with success: " + order);
                    }
                    catch (Exception erro) {
                    
                        Console.WriteLine("Tryed read the result but: "+erro.Message);
                    }
                    
                     /*
                    while (rdr.Read())
                    {
                            Console.WriteLine("erro in loop read");

                            if (rdr.FieldCount > 0) // Checking if the result set has two columns
                            {
                                Console.WriteLine("SUCCESS READER");

                                var fieldCount = rdr.FieldCount;
                                for (global::System.Int32 i = 0; i < fieldCount; i++)
                            {
                                Console.WriteLine(rdr.GetValue(i));

                            }
                           

                        }
                        else if (rdr.IsDBNull(0))
                            {
                                Console.WriteLine("READER NULL");

                            var fieldCount = rdr.FieldCount;
                            for (global::System.Int32 i = 0; i < fieldCount; i++)
                            {
                                Console.WriteLine(rdr.GetValue(i));

                            }
                        }
                        else
                            {
                                Console.WriteLine("erro");
                            }
                    }*/
                }

                

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"useStorageProcedure AddOrder SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" useStorageProcedure AddOrder Error: {ex.Message}");
            }

        }
    }

}