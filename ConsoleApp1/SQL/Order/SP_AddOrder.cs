using Microsoft.Data.SqlClient;
using SQL;
using System.Data;

/*
USE BikeStores
SP_HELP ORDERS
SELECT TOP 1 * FROM ORDERS ORDER BY ORDER_ID DESC
SELECT * FROM STOCKS WHERE product_id = 1 AND store_id = 1
SELECT TOP 1 * FROM ORDER_ITEMS ORDER BY ORDER_ID DESC

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

namespace ConsoleApp1.SQL.Order
{

    public class SP_AddOrder : SqlAccess

    {
        private string procedureName = "AddOrder";

        private string CUS_FIR_NAME { get; set; }
        private string CUS_LAS_NAME { get; set; }
        private string STORE_NAME { get; set; }
        private string STAF_FIR_NAME { get; set; }
        private string STAF_LAS_NAME { get; set; }
        private string ORDER_STATUS { get; set; }
        private string ORDER_DATE { get; set; }
        private string REQUIRED_DATE { get; set; }
        private string SHIPPED_DATE { get; set; }
        private string PRODUCT_NAME { get; set; }
        private string DISCOUNT { get; set; }
        private string QUANTITY { get; set; }

        public void setOrderInformation()
        {
            Console.WriteLine("Order information");
            Console.Write("Customer First Name: ");
            CUS_FIR_NAME = Console.ReadLine();

            Console.Write("Customer Last Name: ");
            CUS_LAS_NAME = Console.ReadLine();

            Console.Write("Product Name: ");
            PRODUCT_NAME = Console.ReadLine();

            Console.Write("Discount: ");
            DISCOUNT = Console.ReadLine();

            Console.Write("Quantity: ");
            QUANTITY = Console.ReadLine();

            Console.Write("Store Name: ");
            STORE_NAME = Console.ReadLine();

            Console.Write("Staff First Name: ");
            STAF_FIR_NAME = Console.ReadLine();

            Console.Write("Staff Last Name: ");
            STAF_LAS_NAME = Console.ReadLine();

            Console.Write("Order Status: ");
            ORDER_STATUS = Console.ReadLine();

            Console.Write("Order Date: ");
            ORDER_DATE = Console.ReadLine();

            Console.Write("Required Date: ");
            REQUIRED_DATE = Console.ReadLine();

            Console.Write("Shipped Date: ");
            SHIPPED_DATE = Console.ReadLine();
        }

        public class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public string OrderStatus { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime? RequiredDate { get; set; }
            public DateTime? ShippedDate { get; set; }
            public int StoreId { get; set; }
            public int StaffId { get; set; }
            public int ItemId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal ListPrice { get; set; }
            public decimal Discount { get; set; }

            public string orderInString()
            {
                return $"OrderId: {OrderId}, CustomerId: {CustomerId}, OrderStatus: {OrderStatus}, " +
                       $"OrderDate: {OrderDate}, RequiredDate: {RequiredDate?.ToString() ?? "null"}, " +
                       $"ShippedDate: {ShippedDate?.ToString() ?? "null"}, StoreId: {StoreId}, " +
                       $"StaffId: {StaffId}, ItemId: {ItemId}, ProductId: {ProductId}, " +
                       $"Quantity: {Quantity}, ListPrice: {ListPrice}, Discount: {Discount}";
            }
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
                OrderStatus = reader.GetString(reader.GetOrdinal("order_status")),
                OrderDate = reader.GetDateTime(reader.GetOrdinal("order_date")),
                RequiredDate = reader.IsDBNull(reader.GetOrdinal("required_date")) ? null : reader.GetDateTime(reader.GetOrdinal("required_date")),
                ShippedDate = reader.IsDBNull(reader.GetOrdinal("shipped_date")) ? null : reader.GetDateTime(reader.GetOrdinal("shipped_date")),
                StoreId = reader.GetInt32(reader.GetOrdinal("store_id")),
                StaffId = reader.GetInt32(reader.GetOrdinal("staff_id")),
                ItemId = reader.GetInt32(reader.GetOrdinal("item_id")),
                ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                ListPrice = reader.GetDecimal(reader.GetOrdinal("list_price")),
                Discount = reader.GetDecimal(reader.GetOrdinal("discount"))
            };
            /* Int32 order_id = rdr.GetInt32(rdr.GetOrdinal("order_id"));
             Console.WriteLine("order_id" + order_id);

             Int32 customerId = rdr.GetInt32(rdr.GetOrdinal("customer_id"));
             Console.WriteLine("customerId: " + customerId);

             string orderStatus = rdr.GetString(rdr.GetOrdinal("order_status"));
             Console.WriteLine("orderStatus: " + orderStatus);

             DateTime orderDate = rdr.GetDateTime(rdr.GetOrdinal("order_date"));
             Console.WriteLine("orderDate: " + orderDate);

             DateTime? requiredDate = rdr.IsDBNull(rdr.GetOrdinal("required_date")) ? (DateTime?)null : rdr.GetDateTime(rdr.GetOrdinal("required_date"));
             Console.WriteLine("requiredDate: " + (requiredDate.HasValue ? requiredDate.Value.ToString() : "null"));

             DateTime? shippedDate = rdr.IsDBNull(rdr.GetOrdinal("shipped_date")) ? (DateTime?)null : rdr.GetDateTime(rdr.GetOrdinal("shipped_date"));
             Console.WriteLine("shippedDate: " + (shippedDate.HasValue ? shippedDate.Value.ToString() : "null"));

             Int32 storeId = rdr.GetInt32(rdr.GetOrdinal("store_id"));
             Console.WriteLine("storeId: " + storeId);

             Int32 staffId = rdr.GetInt32(rdr.GetOrdinal("staff_id"));
             Console.WriteLine("staffId: " + staffId);

             Int32 itemId = rdr.GetInt32(rdr.GetOrdinal("item_id"));
             Console.WriteLine("itemId: " + itemId);

             Int32 productId = rdr.GetInt32(rdr.GetOrdinal("product_id"));
             Console.WriteLine("productId: " + productId);

             Int32 quantity = rdr.GetInt32(rdr.GetOrdinal("quantity"));
             Console.WriteLine("quantity: " + quantity);

             Decimal listPrice = rdr.GetDecimal(rdr.GetOrdinal("list_price"));
             Console.WriteLine("listPrice: " + listPrice);

             Decimal discount = rdr.GetDecimal(rdr.GetOrdinal("discount"));
             Console.WriteLine("discount: " + discount);*/


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
                    addSqlParam("@CUS_FIR_NAME", SqlDbType.NVarChar, CUS_FIR_NAME, cmd);
                    addSqlParam("@CUS_LAS_NAME", SqlDbType.NVarChar, CUS_LAS_NAME, cmd);
                    addSqlParam("@STORE_NAME", SqlDbType.NVarChar, STORE_NAME, cmd);
                    addSqlParam("@STAF_FIR_NAME", SqlDbType.NVarChar, STAF_FIR_NAME, cmd);
                    addSqlParam("@STAF_LAS_NAME", SqlDbType.NVarChar, STAF_LAS_NAME, cmd);

                    // Order Data
                    addSqlParam("@ORDER_STATUS", SqlDbType.Int, ORDER_STATUS, cmd);
                    addSqlParam("@ORDER_DATE", SqlDbType.Date, ORDER_DATE, cmd);
                    addSqlParam("@REQUIRED_DATE", SqlDbType.Date, REQUIRED_DATE, cmd);
                    addSqlParam("@SHIPPED_DATE", SqlDbType.Date, SHIPPED_DATE, cmd);

                    // Item Order
                    addSqlParam("@PRODUCT_NAME", SqlDbType.NVarChar, PRODUCT_NAME, cmd);
                    addSqlParam("@DISCOUNT", SqlDbType.Float, DISCOUNT, cmd);
                    addSqlParam("@QUANTITY", SqlDbType.Int, QUANTITY, cmd);


                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        if (rdr.FieldCount > 0)
                        {


                            Order order = ReadOrder(rdr);
                            Console.WriteLine("order received with success: " + order.orderInString());


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
                Console.WriteLine($"useStorageProcedure AddOrder SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" useStorageProcedure AddOrder Error: {ex.Message}");
            }
            con.Close();


        }
    }

}