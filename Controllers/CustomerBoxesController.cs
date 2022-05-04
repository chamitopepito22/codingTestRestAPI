
using EmployeeRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CustomerBoxesController : Controller
    {
        private readonly DBContext dBContext;
        public CustomerBoxesController(Models.DBContext CustomerBoxesDBContext)
        {
            this.dBContext = CustomerBoxesDBContext;
        }

        [HttpGet("GetAll")]
        public Object GetAll()
        {
            CustomerBoxes CustomerBoxes = new CustomerBoxes(this.dBContext);
            List<CustomerBoxes> lst = CustomerBoxes.GetAll();
            return lst;
        }
        //Add CustomerBoxes
        [HttpPost("Add")]
        public bool Add([FromBody] CustomerBox customerBox)
        {
            try
            {
                return dBContext.CustomerBoxesStorePackage(customerBox);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Add CustomerBoxes
        [HttpPost("Retrieve")]
        public bool Retrieve([FromBody] CustomerBox customerBox)
        {
            try
            {
                return dBContext.CustomerBoxesRetrievePackage(customerBox);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete("Delete")]
        public bool Delete(int Id)
        {
            try
            {
                CustomerBoxes CustomerBoxes = new CustomerBoxes(this.dBContext);
                CustomerBoxes.Id = Id;
                CustomerBoxes.Delete(CustomerBoxes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpGet("GetCustomerBoxesDetails")]
        public async Task<IEnumerable<CustomerBoxDetails>> GetCustomerBoxesDetails(int CustomerId = -1, int FacilityId = -1, int BoxType = -1, string Notes = "")
        {
            if (Notes == null)
                Notes = "";

            if (CustomerId == null)
                CustomerId = -1;

            if (FacilityId == null)
                FacilityId = -1;

            if (BoxType == null)
                BoxType = -1;

            List<CustomerBoxDetails> lst = await dBContext.GetCustomerBoxesDetails(CustomerId, FacilityId, BoxType, Notes);
            return lst;
        }
    }
}
