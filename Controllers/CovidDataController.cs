using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidDataController : ControllerBase
    {
        // GET: api/<CovidDataController>
        //[HttpGet]
        [HttpPost("upload", Name = "upload")]
        public IEnumerable<string> Get(IFormFile UploadedFile)
        {
            if (UploadedFile.FileName.EndsWith(".csv"))
            {
                using (var sreader = new StreamReader(UploadedFile.OpenReadStream()))
                {
                    string[] headers = sreader.ReadLine().Split(',');
                    return headers;//Title
                    while (!sreader.EndOfStream)                          //get all the content in rows 
                    {
                        string[] rows = sreader.ReadLine().Split(',');
                        int Id = int.Parse(rows[0].ToString());
                        int NUM = int.Parse(rows[1].ToString());
                    }
                    
                }
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/<CovidDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CovidDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CovidDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CovidDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
