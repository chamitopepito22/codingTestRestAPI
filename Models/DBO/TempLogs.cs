using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Models
{
    [Table("TempLogs", Schema = "dbo")]
    public class TempLogs
    {
        DBContext _dbContext;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TempLogId { get; set; } //(int, not null)
        public int EmployeeId { get; set; } //(int, not null)
        [Required]
        [Display(Name = "Temperature")]
        public decimal Temperature { get; set; } //(decimal(18,0), not null)
        [Required]
        [Display(Name = "RecordDate")]
        public DateTime RecordDate { get; set; } //(datetime, not null)
        [Required]
        [Display(Name = "TempType")]
        public Int16 TempType { get; set; } //(datetime, not null)

        public enum TempTypes
        {
            TooCold = 0,
            Normal = 1,
            TooHot = 2
        }
        public TempLogs(DBContext DBContext)
        {
            Temperature = 36.5M;
            RecordDate = DateTime.UtcNow;
            TempType = 1;
            _dbContext = DBContext;
        }

        public async Task<TempLogs> Create(TempLogs _object)
        {
            var obj = await _dbContext._TempLogs.AddAsync(_object);

            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(TempLogs _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }
        public bool Update(TempLogs _object)
        {
            try
            {
                
                var result = _dbContext._TempLogs.SingleOrDefault(b => b.TempLogId == _object.TempLogId);
                if (result != null)
                {
                    try
                    {
                        result.Temperature = _object.Temperature;
                        _dbContext._TempLogs.Update(result);
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
        public List<TempLogs> GetAll()
        {
            try
            {
                return _dbContext._TempLogs.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<TempLogs> GetByEmployeeId(int EmployeeId)
        {
            return _dbContext._TempLogs.Where(x => x.EmployeeId == EmployeeId).ToList();
        }
    }
}
