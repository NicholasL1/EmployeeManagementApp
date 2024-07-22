using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeManagementApp.Pages.Employees
{
	public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Server=localhost;Database=mystore;User Id=SA;Password=Speedy109901;";
                ;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.email = reader.GetString(2);
                                employeeInfo.phone = reader.GetString(3);
                                employeeInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        public void OnPost()
        {
            employeeInfo.id = Request.Form["id"];
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.phone = Request.Form["phone"];
            employeeInfo.address = Request.Form["address"];

            if (employeeInfo.id.Length == 0 || employeeInfo.name.Length == 0 || employeeInfo.email.Length == 0 || employeeInfo.phone.Length == 0 || employeeInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
                String connectionString = "Server=localhost;Database=mystore;User Id=SA;Password=Speedy109901;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", employeeInfo.name);
                        command.Parameters.AddWithValue("@email", employeeInfo.email);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);
                        command.Parameters.AddWithValue("@id", employeeInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            Response.Redirect("/Employees/Index");
        }
    }
}
