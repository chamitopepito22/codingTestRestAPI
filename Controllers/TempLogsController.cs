using EmployeeRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class TempLogsController : ControllerBase
    {
        private readonly DBContext dBContext;


        //private readonly IRepository<Models.TempLogs> _TempLogs;


        public TempLogsController(Models.DBContext TempLogsDBContext)
        {
            this.dBContext = TempLogsDBContext;
        }

        [HttpGet("GetAll")]
        public Object GetAll()
        {
            TempLogs TempLogs = new TempLogs(this.dBContext);
            List<TempLogs> lst = TempLogs.GetAll();
            return lst;
        }
        //Add TempLogs
        [HttpPost("Add")]
        public async Task<bool> Add(int EmployeeId, decimal Temperature)
        {
            try
            {
                TempLogs TempLogs = new TempLogs(this.dBContext);
                TempLogs.EmployeeId= EmployeeId;
                TempLogs.Temperature = Temperature;
                TempLogs.RecordDate = DateTime.UtcNow;
                //dBContext._TempLogs.Add(TempLogs);
                await TempLogs.Create(TempLogs);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        [HttpPost("Update")]
        public bool Update(int TempLogId, decimal Temperature)
        {
            try
            {
                TempLogs TempLogs = new TempLogs(this.dBContext);
                TempLogs.TempLogId = TempLogId;
                TempLogs.Temperature = Temperature;
                TempLogs.Update(TempLogs);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        //Delete TempLogs
        [HttpDelete("Delete")]
        public bool Delete(int TempLogId)
        {
            try
            {
                TempLogs TempLogs = new TempLogs(this.dBContext);
                TempLogs.TempLogId = TempLogId;
                TempLogs.Delete(TempLogs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpGet("GetByEmployeeId")]
        public Object GetByEmployeeId(int EmployeeId)
        {
            TempLogs TempLogs = new TempLogs(this.dBContext);
            List<TempLogs> lst = TempLogs.GetByEmployeeId(EmployeeId);
            return lst;
        }

    }


}
