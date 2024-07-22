using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeManagementApp.Pages.Employees
{
	public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.phone= Request.Form["phone"];
            employeeInfo.address= Request.Form["address"];

            if (employeeInfo.name.Length == 0 || employeeInfo.email.Length == 0 || employeeInfo.phone.Length == 0 || employeeInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            // save the new employee into the database

            try
            {
                String connectionString = "Server=localhost;Database=mystore;User Id=SA;Password=Speedy109901;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", employeeInfo.name);
                        command.Parameters.AddWithValue("@email", employeeInfo.email);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            employeeInfo.name = "";
            employeeInfo.email= "";
            employeeInfo.phone = "";
            employeeInfo.address = "";
            successMessage = "New Employee Added Successfully.";

            Response.Redirect("/Employees/Index");
        }

    }
}
