using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeRestAPI.Models
{
    public class CustomerBoxDetails: CustomerBox
    {
        public string FacilityName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }

        public string CreatedDate { get; set; }
    }
    public class CustomerBox
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int FacilityId { get; set; }
        public Int16 BoxType { get; set; }
        public string Notes { get; set; }

        public Int16 StorageStatus { get; set; }
        
    }

    [Table("CustomerBoxes", Schema = "dbo")]
    public class CustomerBoxes
    {
        DBContext _dbContext;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Required]
        [Display(Name = "CustomerId")]
        public int CustomerId { get; set; } 

        [Required]
        [Display(Name = "FacilityId")]
        public int FacilityId { get; set; }

      

        [Required]
        [Display(Name = "BoxType")]
        public Int16 BoxType { get; set; }


        [Required]
        [Display(Name = "StorageStatus")]
        public Int16 StorageStatus { get; set; }
        

        [Required]
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public CustomerBoxes(DBContext DBContext)
        {
            Id = -1;
            CustomerId = -1;
            FacilityId = -1;
            BoxType = -1;
            CreatedDate = DateTime.UtcNow;
            Notes = "";
            _dbContext = DBContext;
        }
        public async Task<CustomerBoxes> Create(CustomerBoxes _object)
        {
            var obj = await _dbContext._CustomerBoxes.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }


        public void Delete(CustomerBoxes _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }
       
        public List<CustomerBoxes> GetAll()
        {
            try
            {
                return _dbContext._CustomerBoxes.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<CustomerBoxes> GetByFacilityId(int FacilityId)
        {
            return _dbContext._CustomerBoxes.Where(x => x.FacilityId == FacilityId).ToList();
        }

        public List<CustomerBoxes> GetByCustomerId(int CustomerId)
        {
            return _dbContext._CustomerBoxes.Where(x => x.CustomerId == CustomerId).ToList();
        }

    }
}
