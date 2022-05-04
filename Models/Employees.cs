using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace EmployeeRestAPI.Models
{
   
    public class EmployeeFilter 
    {
        public int EmployeeId { get; set; } //(int, not null)
        public string FirstName { get; set; } //(nvarchar(50), not null)
        public string LastName { get; set; } //(nvarchar(50), not null)
        public new DateTime? DateFrom { get; set; }
        public new DateTime? DateTo { get; set; }
        public decimal? TemperatureFrom { get; set; } //(decimal(18,0), null)
        public decimal? TemperatureTo { get; set; } //(decimal(18,0), null)
        public EmployeeFilter()
        {
            EmployeeId = -1;
            FirstName = "";
            LastName = "";
   
        }


    }

    public class Employee_Get
    {
        public int EmployeeId { get; set; } //(int, not null)
        public string FirstName { get; set; } //(nvarchar(50), not null)
        public string LastName { get; set; } //(nvarchar(50), not null)
        public DateTime CreateDate { get; set; } //(datetime, not null)
        public DateTime UpdateDate { get; set; } //(datetime, not null)
        public decimal? Temperature { get; set; } //(decimal(18,0), null)
        public DateTime? RecordDate { get; set; } //(datetime, null)
        public Int16? TempType { get; set; } //(datetime, not null)



    }


    public class Customers_Get
    {
        public int Id { get; set; } //(int, not null)
        public string FirstName { get; set; } //(nvarchar(50), not null)
        public string LastName { get; set; } //(nvarchar(50), not null)
        public string PhoneNumber { get; set; } //(nvarchar(50), not null)
        public int TotalBoxes { get; set; } //(int, not null)



    }
}
