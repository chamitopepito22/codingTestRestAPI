using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Models
{
    [Table("CovidData", Schema = "dbo")]
    public class CovidData
    {
        DBContext _dbContext;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        [Display(Name = "SNo")]
        public int SNo { get; set; } 

        [Required]
        [Display(Name = "ObservationDate")]
        public DateTime ObservationDate { get; set; }


        [Required]
        [Display(Name = "ProvinceState")]
        public string ProvinceState { get; set; } 

        [Required]
        [Display(Name = "CountryRegion")]
        public string CountryRegion { get; set; } 


        [Required]
        [Display(Name = "LastUpdate")]
        public DateTime LastUpdate { get; set; } 

        [Required]
        [Display(Name = "Confirmed")]
        public int Confirmed { get; set; }


        [Required]
        [Display(Name = "Deaths")]
        public int Deaths { get; set; }

        [Required]
        [Display(Name = "Recovered")]
        public int Recovered { get; set; }


        public CovidData(DBContext DBContext)
        {
            SNo = -1;
            ObservationDate = DateTime.UtcNow;
            ProvinceState = "";
            CountryRegion = "";
            LastUpdate = DateTime.UtcNow;
            Confirmed = 0;
            Deaths = 0;
            Recovered = 0;
            _dbContext = DBContext;
        }
        public async Task<CovidData> Create(CovidData _object)
        {
            var obj = await _dbContext._CovidData.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }


    }
}
