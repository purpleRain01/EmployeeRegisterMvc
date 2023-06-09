using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SunExample.Models;

namespace SunExample.DAL
{
    public class Employee_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["Defaultcon"].ToString();

        //getting employees all
        public List<Employee> GetallEmplpoyees()
        {
            List<Employee> employeeList = new List<Employee>();
                
                using(SqlConnection connection=new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetallEmmployees";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                sqlDA.Fill(dt);
                connection.Close();


                foreach(DataRow dr in dt.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Serno=Convert.ToInt32(dr["Id"]),
                        EmpID=dr["EmployeeId"].ToString(),
                        EmpName = dr["EmployeeName"].ToString(),
                        Dob=dr["DOB"].ToString(),
                        PassNo= dr["PassportNO"].ToString(),
                        Nationality= dr["Nationality"].ToString(),
                        Pob= dr["POB"].ToString(),
                        VisaId=dr["VisaID"].ToString(),
                        VisaType= dr["Vistype"].ToString(),
                        Branch= dr["Branch"].ToString(),
                        BasicSalary= Convert.ToDecimal(dr["BasicSalary"]),
                        Allowance= Convert.ToDecimal(dr["Allowance"]),
                        Salarydeduct= Convert.ToDecimal(dr["Salarydeduct"]),
                        NetSalary= Convert.ToDecimal(dr["Netsalary"]),
                        Status=(dr["status"]).ToString()


                    }); ;
                }
            }
            return employeeList;
        }

        //inserting emp fn
        public bool InsertEmployee(Employee employee)
        {
            int retmsg = 0;
            using(SqlConnection connection=new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empname", employee.EmpName);
                command.Parameters.AddWithValue("@dob", employee.Dob);
                command.Parameters.AddWithValue("@passno", employee.PassNo);
                command.Parameters.AddWithValue("@nationality", employee.Nationality);
                command.Parameters.AddWithValue("@pob", employee.Pob);
                command.Parameters.AddWithValue("@visaid", employee.VisaId);
                command.Parameters.AddWithValue("@vistype", employee.VisaType);
                command.Parameters.AddWithValue("@branch", employee.Branch);
                command.Parameters.AddWithValue("@basicsal", employee.BasicSalary);
                command.Parameters.AddWithValue("@allowance", employee.Allowance);
                command.Parameters.AddWithValue("@salded", employee.Salarydeduct);
                command.Parameters.AddWithValue("@status", "Active");

                connection.Open();
                retmsg = command.ExecuteNonQuery();
                connection.Close();
                
            }
            if (retmsg > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //getting employee by id
        public List<Employee> GetEmplpoyeebyID(string Empid)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetEmployeebyID";
                command.Parameters.AddWithValue("@id", Empid);

                SqlParameter flagParameter = command.Parameters.Add("@flag", SqlDbType.Bit);
                flagParameter.Direction = ParameterDirection.Output;

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                sqlDA.Fill(dt);
                bool flagValue = (bool)flagParameter.Value;
                connection.Close();

                if (flagValue == false)
                {
                    return employeeList;
                }



                foreach (DataRow dr in dt.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Serno = Convert.ToInt32(dr["Id"]),
                        EmpID = dr["EmployeeId"].ToString(),
                        EmpName = dr["EmployeeName"].ToString(),
                        Dob = dr["DOB"].ToString(),
                        PassNo = dr["PassportNO"].ToString(),
                        Nationality = dr["Nationality"].ToString(),
                        Pob = dr["POB"].ToString(),
                        VisaId = dr["VisaID"].ToString(),
                        VisaType = dr["Vistype"].ToString(),
                        Branch = dr["Branch"].ToString(),
                        BasicSalary = Convert.ToDecimal(dr["BasicSalary"]),
                        Allowance = Convert.ToDecimal(dr["Allowance"]),
                        Salarydeduct = Convert.ToDecimal(dr["Salarydeduct"]),
                        NetSalary = Convert.ToDecimal(dr["Netsalary"]),
                        Status = (dr["status"]).ToString()


                    }); ;
                }
            }
            return employeeList;
        }

        public bool UpdateEmployee(Employee employee)
        {
            int retmsg1 = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empid", employee.EmpID);
                command.Parameters.AddWithValue("@empname", employee.EmpName);
                command.Parameters.AddWithValue("@dob", employee.Dob);
                command.Parameters.AddWithValue("@passno", employee.PassNo);
                command.Parameters.AddWithValue("@nationality", employee.Nationality);
                command.Parameters.AddWithValue("@pob", employee.Pob);
                command.Parameters.AddWithValue("@visaid", employee.VisaId);
                command.Parameters.AddWithValue("@vistype", employee.VisaType);
                command.Parameters.AddWithValue("@branch", employee.Branch);
                command.Parameters.AddWithValue("@basicsal", employee.BasicSalary);
                command.Parameters.AddWithValue("@allowance", employee.Allowance);
                command.Parameters.AddWithValue("@salded", employee.Salarydeduct);
                command.Parameters.AddWithValue("@status", employee.Status);

                connection.Open();
                retmsg1 = command.ExecuteNonQuery();
                connection.Close();

            }
            if (retmsg1 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Employee> GetEmplpoyeebyIDdelete(string Empid)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetEmployeebyIDdelete";
                command.Parameters.AddWithValue("@id", Empid);

                SqlParameter flagParameter = command.Parameters.Add("@flag", SqlDbType.Bit);
                flagParameter.Direction = ParameterDirection.Output;

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                sqlDA.Fill(dt);
                bool flagValue = (bool)flagParameter.Value;
                connection.Close();

                if (flagValue == false)
                {
                    return employeeList;
                }



                foreach (DataRow dr in dt.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Serno = Convert.ToInt32(dr["Id"]),
                        EmpID = dr["EmployeeId"].ToString(),
                        EmpName = dr["EmployeeName"].ToString(),
                        Dob = dr["DOB"].ToString(),
                        PassNo = dr["PassportNO"].ToString(),
                        Nationality = dr["Nationality"].ToString(),
                        Pob = dr["POB"].ToString(),
                        VisaId = dr["VisaID"].ToString(),
                        VisaType = dr["Vistype"].ToString(),
                        Branch = dr["Branch"].ToString(),
                        BasicSalary = Convert.ToDecimal(dr["BasicSalary"]),
                        Allowance = Convert.ToDecimal(dr["Allowance"]),
                        Salarydeduct = Convert.ToDecimal(dr["Salarydeduct"]),
                        NetSalary = Convert.ToDecimal(dr["Netsalary"]),
                        Status = (dr["status"]).ToString()


                    }); ;
                }
            }
            return employeeList;
        }

        public string DeleteProduct(string id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empid", id);
                command.Parameters.Add("@msg", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@msg"].Value.ToString();
                connection.Close();
            }

                return result;
        }

    }
}