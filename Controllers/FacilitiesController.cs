
using EmployeeRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class FacilitiesController : Controller
    {
        private readonly DBContext dBContext;
        public FacilitiesController(Models.DBContext FacilitiesDBContext)
        {
            this.dBContext = FacilitiesDBContext;
        }

        [HttpGet("GetAll")]
        public Object GetAll()
        {
            Facilities Facilities = new Facilities(this.dBContext);
            List<Facilities> lst = Facilities.GetAll();
            return lst;
        }
        //Add Facilities
        [HttpPost("Add")]
        public async Task<bool> Add([FromBody] Facility facility)
        {
            try
            {
                Facilities Facilities = new Facilities(this.dBContext);
                Facilities.Name = facility.Name;

                Facilities.SmallMaxCount = facility.SmallMaxCount;
                Facilities.MediumMaxCount = facility.MediumMaxCount;
                Facilities.LargeMaxCount = facility.LargeMaxCount;


                Facilities.SmallCount = 0;
                Facilities.MediumCount = 0;
                Facilities.LargeCount = 0;
                await Facilities.Create(Facilities);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpPost("Update")]
        public bool Update([FromBody] Facility facility)
        {

            try
            {
                Facilities Facilities = new Facilities(this.dBContext);
                Facilities.Id = facility.Id;
                Facilities.Name = facility.Name;

                Facilities.SmallMaxCount = facility.SmallMaxCount;
                Facilities.MediumMaxCount = facility.MediumMaxCount;
                Facilities.LargeMaxCount = facility.LargeMaxCount;


                Facilities.Update(Facilities);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        [HttpPost("Delete")]
        public string Delete([FromBody] Facility facility)
        {
            try
            {
                Facilities Facilities = new Facilities(this.dBContext);
                Facilities.Id = facility.Id;
                return Facilities.Delete(Facilities);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
