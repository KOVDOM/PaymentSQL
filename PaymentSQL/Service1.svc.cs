using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PaymentSQL
{
    public class Service1 : IService1
    {
        List<Customer> customerList = new List<Customer>();
        Connect c = new Connect();

        public List<Customer> GetCustomers()
        {
            string query = "SELECT * FROM customer";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c.connect;
            cmd.CommandText = query;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Customer ctm = new Customer();
                ctm.Id = dr.GetInt32(0);
                ctm.Name = dr.GetString(1);
                ctm.City= dr.GetString(2);
                ctm.Age=dr.GetInt32(3);
                customerList.Add(ctm);
            }

            return customerList;
        }

        public string postCustomer(string name, string city, string age)
        {
            string query = "INSERT INTO customer(name,city,age) VALUE (@name,@city,@age);";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Connection = c.connect;
            cmd.CommandText = query;
            cmd.ExecuteReader();

            return "A felhasználó hozzáadva!";
        }

        //public string putCustomer(Customer customer)
        //{
        //    string query = "INSERT INTO customer(name,city,age) VALUES (@name,@city,@age)";
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.Parameters.AddWithValue("@name", customer.Name);
        //    cmd.Parameters.AddWithValue("@city", customer.City);
        //    cmd.Parameters.AddWithValue("@age", customer.Age);
        //    cmd.Connection=c.connect;
        //    cmd.CommandText = query;
        //    cmd.ExecuteReader();

        //    return "A felhasználó hozzáadva!";
        //}
    }
}
