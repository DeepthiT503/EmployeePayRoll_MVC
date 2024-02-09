using Microsoft.Data.SqlClient;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class EmployeeRepository : IEmployeeRepository
    {
        string connectionString = @"Server=LAPTOP-I34SOOK0\SQLEXPRESS01;Database=MyDatabase;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        public IEnumerable<EmployeeEntity> GetAllEmployees()
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }
        // inserting the values t
        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("InsertEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);


                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return employee;
        }
        public EmployeeEntity UpdateEmployeeDetails(EmployeeEntity employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateEmployee1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            return employee;
        }
        public EmployeeEntity GetEmployeeById(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetEmployeeById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add a parameter for the employeeId
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                // Execute the reader to retrieve data
                SqlDataReader rdr = cmd.ExecuteReader();

                // Check if there are rows returned
                if (rdr.Read())
                {
                    // Create an EmployeeModel object and populate it with data from the reader
                    EmployeeEntity employee = new EmployeeEntity
                    {
                        EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                        FullName = rdr["FullName"].ToString(),
                        ImagePath = rdr["ImagePath"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Department = rdr["Department"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        Notes = rdr["Notes"].ToString()

                    };

                    // Close the reader before returning the result
                    rdr.Close();

                    // Close the connection
                    connection.Close();

                    return employee;
                }

                // If no rows are returned, close the reader and connection and return null
                rdr.Close();
                connection.Close();
                return null;
            }
        }
        public EmployeeEntity GetEmployeeByBoth(string searchString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<EmployeeEntity> employees = new List<EmployeeEntity>();
                connection.Open();

                SqlCommand cmd = new SqlCommand("SearchEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add a parameter for the employeeId
                cmd.Parameters.AddWithValue("@searchString", searchString);
                // Execute the reader to retrieve data
                SqlDataReader rdr = cmd.ExecuteReader();

                // Check if there are rows returned
                while (rdr.Read())
                {
                    // Create an EmployeeModel object and populate it with data from the reader
                    EmployeeEntity employee = new EmployeeEntity
                    {
                        EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                        FullName = rdr["FullName"].ToString(),
                        ImagePath = rdr["ImagePath"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Department = rdr["Department"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        Notes = rdr["Notes"].ToString()

                    };

                    // Close the reader before returning the result
                    rdr.Close();

                    // Close the connection
                    connection.Close();

                    employees.Add(employee);
                }

                // If no rows are returned, close the reader and connection and return null
                rdr.Close();
                connection.Close();
                return null;
            }
        }
        public EmployeeEntity DeleteFromEmployee(int EmployeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DeleteEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                var result = GetEmployeeById(EmployeeId);
                cmd.ExecuteNonQuery();
                connection.Close();
                return result;
            }
            return null;
        }
        //public IEnumerable<EmployeeEntity> GetAllEmployeesByName(string text)
        //{
        //    List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        con.Open();
        //        string sqlQuery = "SELECT * FROM EmployeeTable WHERE FullName LIKE @text OR Department LIKE @text";

        //        SqlCommand cmd = new SqlCommand("SearchEmployee", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@searchString", text);

        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            EmployeeEntity employee = new EmployeeEntity();

        //            employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
        //            employee.FullName = rdr["FullName"].ToString();
        //            employee.ImagePath = rdr["ImagePath"].ToString();
        //            employee.Gender = rdr["Gender"].ToString();
        //            employee.Department = rdr["Department"].ToString();
        //            employee.Salary = Convert.ToDecimal(rdr["Salary"]);
        //            employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
        //            employee.Notes = rdr["Notes"].ToString();
        //            lstemployee.Add(employee);
        //        }
        //        con.Close();
        //    }
        //    return lstemployee;
        //}

        public EmployeeEntity Login(int id, string name)
        {
            EmployeeEntity employee = new EmployeeEntity();


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM EmployeeTable WHERE EmployeeId = " + id + " AND FullName = '" + name + "'";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);


                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();

                }

                if (rdr.HasRows)
                {
                    return employee;
                }
            }
            return null;
        }






    }
}
  
