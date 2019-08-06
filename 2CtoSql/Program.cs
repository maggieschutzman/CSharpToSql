using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace _2CtoSql {
    class Program {
        static void Main(string[] args) {

            var sql = "SELECT * from Customers where State = 'OH';";
            var customers = SelectCustomer(sql);
            foreach (var customer in customers) {
                Console.WriteLine(customer.Name);
            }

            sql = "SELECT * from Orders;";
            var orders = SelectOrder(sql);
            foreach (var Orders in orders) {
                Console.WriteLine(orders);
            }

            var cust = GetCustomerById("2001");
            if (cust == null) {
                Console.WriteLine("Customer not found");
            }
            else {
                Console.WriteLine(cust.Name);
            }

            var ord = GetOrderById("2001");
            if (ord == null) {
                Console.WriteLine("Order not found");
            }
            else {
                Console.WriteLine(ord);
            }
        }

        static List<Order> SelectOrder(string sql) {
            var connStr = @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var orderList = new List<Order>();
            var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note"))
                       ? null
                       : reader["Note"].ToString();
                int customerid;
                if (reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {
                    customerid = 0;
                }
                else {
                    customerid = (int)reader["CustomerId"];
                }
                var order = new Order(id, date, note, customerid);
                orderList.Add(order);
            }
            reader.Close();
            connection.Close();
            return orderList;
        }


        static List<Customer> SelectCustomer(string sql) {
            var connStr = @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var customerList = new List<Customer>();
            var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                       ? null
                       : reader["Code"].ToString();
                var customer = new Customer(id, name, city, state, active, code);
                customerList.Add(customer);
            }

            reader.Close();
            connection.Close();
            return customerList;
        }
        static Customer GetCustomerById(string pid) {
            var connStr = @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var sql = "SELECT * From Customers where Id = @myid;";
            var cmd = new SqlCommand(sql, connection);
            var customerId = new SqlParameter("@myid", pid);
            cmd.Parameters.Add(customerId);
            var reader = cmd.ExecuteReader();
            Customer cust = null;
            if (reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                       ? null
                       : reader["Code"].ToString();
                cust = new Customer(id, name, city, state, active, code);
            }
            reader.Close();
            connection.Close();
            return cust;
        }
        static Order GetOrderById(string pid) {
            var connStr = @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var sql = "SELECT * From Orders where Id = @myid;";
            var cmd = new SqlCommand(sql, connection);
            var orderId = new SqlParameter("@myid", pid);
            cmd.Parameters.Add(orderId);
            var reader = cmd.ExecuteReader();
            Order orders = null;
            if (reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note"))
                       ? null
                       : reader["Note"].ToString();
                int customerid;
                if (reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {
                    customerid = 0;
                }
                else {
                    customerid = (int)reader["CustomerId"];
                }
                orders = new Order(id, date, note, customerid);


            }
            reader.Close();
            connection.Close();
            return orders;
        }
    }
}


