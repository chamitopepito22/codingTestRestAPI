using EmployeeRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContext dBContext;


        //private readonly IRepository<Models.Employees> _Employee;


        public EmployeeController(Models.DBContext employeeDBContext)
        {
            this.dBContext = employeeDBContext;
        }

        [HttpGet("GetAllEmployees")]
        public Object GetAll()
        {
            Employees employees = new Employees(this.dBContext);
            List<Employees> lst = employees.GetAll();
            return lst;
        }
        //Add Employee
        [HttpPost("AddEmployee")]
        public async Task<bool> Add( string FirstName, string LastName)
        {
            try
            {
                Employees employees = new Employees(this.dBContext);
                employees.FirstName = FirstName;    
                employees.LastName = LastName;
                employees.CreateDate = DateTime.UtcNow;
                employees.UpdateDate = DateTime.UtcNow;
                //dBContext._Employees.Add(employees);
                await employees.Create(employees);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        [HttpPost("UpdateEmployee")]
        public bool Update(int EmployeeId, string FirstName, string LastName)
        {
            try
            {
                Employees employees = new Employees(this.dBContext);
                employees.EmployeeId = EmployeeId;
                employees.FirstName = FirstName;
                employees.LastName = LastName;
                employees.Update(employees);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        //Delete Employee
        [HttpDelete("DeleteEmployee")]
        public bool Delete(int EmployeeId)
        {
            try
            {
                Employees employees = new Employees(this.dBContext);
                employees.EmployeeId = EmployeeId;
                employees.Delete(employees);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet("GetLatestTemp")]
        //public async Task<IEnumerable<Employee_Get>> GetLatestTemp([FromBody] EmployeeFilter employeeFilter)
        public async Task<IEnumerable<Employee_Get>> GetLatestTemp(DateTime? DateFrom = null, DateTime? DateTo = null, decimal? TemperatureFrom = null, decimal? TemperatureTo = null, string FirstName = "", string LastName = "")
        {
            //if (!DateFrom.HasValue)
            //    DateFrom = DateTime.UtcNow.AddDays(-30);

            //if (!DateTo.HasValue)
            //    DateTo = DateTime.UtcNow.AddDays(1);

            EmployeeFilter employeeFilter = new EmployeeFilter();
            employeeFilter.FirstName = FirstName;
            employeeFilter.LastName = LastName;
            employeeFilter.DateFrom = DateFrom;
            employeeFilter.DateTo = DateTo; 
            employeeFilter.TemperatureFrom = TemperatureFrom;
            employeeFilter.TemperatureTo = TemperatureTo;

            List<Employee_Get> lst = await dBContext.GetEmployeeLatestTemp(employeeFilter);
            return lst;
        }

    }


}
