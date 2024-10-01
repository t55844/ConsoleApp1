using SQL;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Microsoft.Identity.Client;
using ProductiesNamesByModelYear;
using AddOrder;


partial class Program
{
    static void Main(string[] args)
    {
        // Define connection string parameters
        Int16 ModelYear = 2017;
        char SqlFilterByOneYear = 'S';

       /* SP_ProductiesNamesByModelYear ProductiesNamesByModelYear = new SP_ProductiesNamesByModelYear();
        ProductiesNamesByModelYear.getData(ModelYear, SqlFilterByOneYear);
        Console.WriteLine("Hello, World!");*/

        SP_AddOrder addOrder = new SP_AddOrder();
        addOrder.getData();
    }
}
