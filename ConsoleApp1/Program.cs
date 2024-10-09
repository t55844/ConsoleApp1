using SQL;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Microsoft.Identity.Client;
using ProductiesNamesByModelYear;
using ConsoleApp1.SQL.Order;



partial class Program
{
    static void Main(string[] args)
    {

       /* SP_ProductiesNamesByModelYear ProductiesNamesByModelYear = new SP_ProductiesNamesByModelYear();
        ProductiesNamesByModelYear.getData(ModelYear, SqlFilterByOneYear);
        Console.WriteLine("Hello, World!");*/

        SP_AddOrder addOrder = new SP_AddOrder();
        addOrder.setOrderInformation();
        addOrder.getData();

        SP_DeleteOrder deleteOrder = new SP_DeleteOrder();
        deleteOrder.setOrderInformation();
        deleteOrder.deleteData();

    }
}
