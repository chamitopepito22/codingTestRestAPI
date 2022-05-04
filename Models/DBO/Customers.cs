using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalBoxes { get; set; }
        
    }


    [Table("Customers", Schema = "dbo")]
    public class Customers
    {
        DBContext _dbContext;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; } //(nvarchar(50), not null)
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "TotalBoxes")]
        public int TotalBoxes { get; set; }

        public Customers(DBContext DBContext)
        {
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            TotalBoxes = 0;
            _dbContext = DBContext;
        }
        public async Task<Customers> Create(Customers _object)
        {
            var obj = await _dbContext._Customers.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public bool Delete(Customers _object)
        {
            //try
            //{
            //    return _dbContext.Customer_Delete(_object.Id);
            //}
            //catch (Exception)
            //{

            //}
            //_dbContext.Remove(_object);
            //_dbContext.SaveChanges();

            var result = _dbContext._Customers.SingleOrDefault(b => b.Id == _object.Id);
            if (result != null)
            {
                if (result.TotalBoxes > 0)
                    return false;

                var box_result = _dbContext._CustomerBoxes.SingleOrDefault(b => b.CustomerId == _object.Id);
                if(box_result != null)
                    return false;
                try
                {
                    _dbContext.Remove(result);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return false;
        }
        public bool Update(Customers _object)
        {
            try
            {
                var result = _dbContext._Customers.SingleOrDefault(b => b.Id == _object.Id);
                if (result != null)
                {
                    try
                    {
                        result.FirstName = _object.FirstName;
                        result.LastName = _object.LastName;
                        result.PhoneNumber = _object.PhoneNumber;
                        _dbContext._Customers.Update(result);
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
        public List<Customers> GetAll()
        {
            try
            {
                return _dbContext._Customers.ToList();
                //return _dbContext._Customers.OrderByDescending(s => s.Id).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public Customers GetById(int Id)
        {
            return _dbContext._Customers.Where(x => x.Id == Id).FirstOrDefault();
        }
    }
}
