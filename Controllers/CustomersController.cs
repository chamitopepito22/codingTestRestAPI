using EmployeeRestAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CustomersController : Controller
    {
        private readonly DBContext dBContext;
        public CustomersController(Models.DBContext CustomersDBContext)
        {
            this.dBContext = CustomersDBContext;
        }

        [HttpGet("GetAll")]
        public Object GetAll()
        {
            Customers Customers = new Customers(this.dBContext);
            List<Customers> lst = Customers.GetAll();
            return lst;
        }

        [HttpGet("GetById")]
        public Object GetById(Customer _customer)
        {
            Customers Customers = new Customers(this.dBContext);
            return Customers.GetById(_customer.Id);
        }
        //Add Customers
        [HttpPost("Add")]
        public async Task<bool> Add([FromBody] Customer _customer)
        {
            try
            {
                Customers Customers = new Customers(this.dBContext);
                Customers.FirstName = _customer.FirstName;
                Customers.LastName = _customer.LastName;
                Customers.PhoneNumber = _customer.PhoneNumber;
                await Customers.Create(Customers);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        [HttpGet("GetAllWithBoxCount")]
        public async Task<IEnumerable<Customers_Get>> GetAllWithBoxCount()
        {
            List<Customers_Get> lst = await dBContext.GetCustomersAllWithBoxCount();
            return lst;
        }



        [HttpPost("Update")]
        public bool Update([FromBody] Customer _customer)
        {
            try
            {
                Customers Customers = new Customers(this.dBContext);
                Customers.Id = _customer.Id;
                Customers.FirstName = _customer.FirstName;
                Customers.LastName = _customer.LastName;
                Customers.PhoneNumber = _customer.PhoneNumber;
                Customers.Update(Customers);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        [HttpPost("DeleteInfo")]
        public bool DeleteInfo([FromBody] Customer _customer)
        {
            try 
            {
                Customers Customers = new Customers(this.dBContext);
                Customers.Id = _customer.Id;
                return Customers.Delete(Customers);
                
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}