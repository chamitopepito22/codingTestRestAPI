using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Models
{
    [Table("Employees", Schema = "dbo")]
    public class Employees
    {
        DBContext _dbContext;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; } //(int, not null)
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; } //(nvarchar(50), not null)
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; } //(nvarchar(50), not null)
        public DateTime CreateDate { get; set; } //(datetime, not null)
        public DateTime UpdateDate { get; set; } //(datetime, not null)

        public Employees(DBContext DBContext)
        {
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
            FirstName = "";
            LastName = "";
            _dbContext = DBContext;
        }
        public async Task<Employees> Create(Employees _object)
        {
            var obj = await _dbContext._Employees.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Employees _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }
        public bool Update(Employees _object)
        {
            try
            {

                var result = _dbContext._Employees.SingleOrDefault(b => b.EmployeeId == _object.EmployeeId);
                if (result != null)
                {
                    try
                    {
                        result.FirstName = _object.FirstName;
                        result.LastName = _object.LastName;
                        _dbContext._Employees.Update(result);
                        _dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<Employees> GetAll()
        {
            try
            {
                return _dbContext._Employees.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public Employees GetById(int EmployeeId)
        {
            return _dbContext._Employees.Where(x => x.EmployeeId == EmployeeId).FirstOrDefault();
        }

    }
}
