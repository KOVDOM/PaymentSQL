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

        public string putCustomer(Customer cust)
        {
            try
            {
                string query = "INSERT INTO customer(name,city,age) VALUES (@name,@city,@age)";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@name", cust.Name);
                cmd.Parameters.AddWithValue("@city", cust.City);
                cmd.Parameters.AddWithValue("@age", cust.Age);
                cmd.Connection = c.connect;
                cmd.CommandText = query;
                cmd.ExecuteReader();

                return "A felhasználó hozzáadva!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string deletecustomer(string id)
        {

            string query = "DELETE FROM `customer` WHERE `id`=@id;";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c.connect;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            return "A felhasználó törölve!";
        }

        public string updateCustomer(Customer cust)
        {
            try
            {
                string query = "UPDATE `customer` SET `name`=@name,`city`=@city,`age`=@age WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = c.connect;
                cmd.Parameters.AddWithValue("@id", cust.Id);
                cmd.Parameters.AddWithValue("@name", cust.Name);
                cmd.Parameters.AddWithValue("@city", cust.City);
                cmd.Parameters.AddWithValue("@age", cust.Age);
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                return "A felhasználó modosítva!";  
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
