using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunExample.DAL;
using SunExample.Models;

namespace SunExample.Controllers
{
    public class EmployeeController : Controller
    {
        Employee_DAL employee_DAL = new Employee_DAL();
        // GET: Employee
        public ActionResult Index()
        {
            var employeeList = employee_DAL.GetallEmplpoyees();
            if (employeeList.Count == 0)
            {
                TempData["msg"] = "Currently Employees are not available.";
            }


            return View(employeeList);
        }




        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }


        public ActionResult Create()
        {
            string conString = ConfigurationManager.ConnectionStrings["Defaultcon"].ToString();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM dbo.tbl_Countries";

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);

                connection.Open();
                sqlDA.Fill(dt);

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM dbo.tbl_Branch";
                sqlDA.Fill(dt1);

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT MAX(Id) as Id FROM dbo.tbl_EmpMaster";
                sqlDA.Fill(dt2);
            }

            ViewBag.Nationalities = dt.AsEnumerable().Select(row => new SelectListItem
            {
                Value = row["Nationality"].ToString(),
                Text = row["Nationality"].ToString()
            }).ToList();

            ViewBag.Branches = dt1.AsEnumerable().Select(row => new SelectListItem
            {
                Value = row["Branch"].ToString(),
                Text = row["Branch"].ToString()
            }).ToList();

            int emid =Convert.ToInt32(dt2.Rows[0]["Id"]);

            int currentYear = DateTime.Now.Year % 100;

            string empid = currentYear + "EMP000" + (emid+1);
            ViewBag.empId = empid;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            bool isInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = employee_DAL.InsertEmployee(employee);
                    if (isInserted)
                    {
                        TempData["succmsg"] = "Employee data inserted";
                    }
                    else
                    {
                        TempData["Errmsg"] = "Employee data insertion failed";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Errmsg"] = ex.Message;
                return View();
            }
        }


     
      

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            var employee = employee_DAL.GetEmplpoyeebyID(id).FirstOrDefault();
            if (employee==null)
            {
                TempData["msg"] = "Employee canot be edited "+id.ToString();
                return RedirectToAction("Index");
            }

            string conString = ConfigurationManager.ConnectionStrings["Defaultcon"].ToString();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM dbo.tbl_Countries";

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);

                connection.Open();
                sqlDA.Fill(dt);

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM dbo.tbl_Branch";
                sqlDA.Fill(dt1);

                ViewBag.Nationalities = dt.AsEnumerable().Select(row => new SelectListItem
                {
                    Value = row["Nationality"].ToString(),
                    Text = row["Nationality"].ToString()
                }).ToList();

                ViewBag.Branches = dt1.AsEnumerable().Select(row => new SelectListItem
                {
                    Value = row["Branch"].ToString(),
                    Text = row["Branch"].ToString()
                }).ToList();

                return View(employee);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = employee_DAL.UpdateEmployee(employee);
                    if (IsUpdated)
                    {
                        TempData["succmsg"] = "Employee Updated successfully";
                    }
                    else
                    {
                        TempData["Errmsg"] = "Unable to Update employee";
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Errmsg"] = ex.Message;
                return View();
            }
           

            
        }







        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            var employee = employee_DAL.GetEmplpoyeebyIDdelete(id).FirstOrDefault();
            if (employee == null)
            {
                TempData["msg"] = "Employee canot be Deleted " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(string id)
        {
            string result = employee_DAL.DeleteProduct(id);
            if(result.Contains("Deletion"))
            {
                TempData["succmsg"] = result;
            }
            else
            {
                TempData["Errmsg"] = result;
            }
            return RedirectToAction("Index");
            
        }
    }
}
