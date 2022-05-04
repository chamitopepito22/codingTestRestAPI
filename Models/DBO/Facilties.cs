using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeRestAPI.Models
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SmallCount { get; set; }
        public int MediumCount { get; set; }
        public int LargeCount { get; set; }

        public int SmallMaxCount { get; set; }
        public int MediumMaxCount { get; set; }
        public int LargeMaxCount { get; set; }

    }


    [Table("Facilities", Schema = "dbo")]

    public class Facilities
    {
        DBContext _dbContext;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
       
        [Required]
        [Display(Name = "SmallCount")]
        public int SmallCount { get; set; }

        [Required]
        [Display(Name = "MediumCount")]

        public int MediumCount { get; set; }

        [Required]
        [Display(Name = "LargeCount")]

        public int LargeCount { get; set; }


        [Required]
        [Display(Name = "SmallMaxCount")]
        public int SmallMaxCount { get; set; }

        [Required]
        [Display(Name = "MediumMaxCount")]

        public int MediumMaxCount { get; set; }

        [Required]
        [Display(Name = "LargeMaxCount")]

        public int LargeMaxCount { get; set; }


        public Facilities(DBContext DBContext)
        {
            //Id = -1;
            Name = "";
            SmallCount = -1;
            MediumCount = -1;
            LargeCount = -1;

            SmallMaxCount = -1;
            MediumMaxCount = -1;
            LargeMaxCount = -1;

            _dbContext = DBContext;
        }
        public async Task<Facilities> Create(Facilities _object)
        {
            var obj = await _dbContext._Facilities.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }



        public string Delete(Facilities _object)
        {
            //_dbContext.Remove(_object);
            //_dbContext.SaveChanges();

            var result = _dbContext._Facilities.SingleOrDefault(b => b.Id == _object.Id);
            if (result != null)
            {
                if (result.SmallCount > 0 || result.MediumCount > 0 || result.LargeCount > 0)
                    return "Unable to delete facility. There are still packages in the facility.";

                var box_result = _dbContext._CustomerBoxes.SingleOrDefault(b => b.FacilityId == _object.Id);
                if (box_result != null)
                    return "Unable to delete facility. There are package infos in the facility. ";

                try
                {
                    _dbContext.Remove(result);
                    _dbContext.SaveChanges();
                    return "success";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }

            return "Something went wrong.";


        }
        public bool Update(Facilities _object)
        {
            try
            {
                var result = _dbContext._Facilities.SingleOrDefault(b => b.Id == _object.Id);
                if (result != null)
                {
                    try
                    {
                        result.Name = _object.Name;
                        result.SmallMaxCount = _object.SmallMaxCount;
                        result.MediumCount = _object.MediumCount;
                        result.LargeMaxCount = _object.LargeMaxCount;

                        _dbContext._Facilities.Update(result);
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
        public List<Facilities> GetAll()
        {
            try
            {
                //return _dbContext._Facilities.OrderBy(x => x.SmallCount).ThenBy(x => x.MediumCount).ThenBy(x => x.LargeCount).ToList();

                return _dbContext._Facilities.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public Facilities GetById(int Id)
        {
            return _dbContext._Facilities.Where(x => x.Id == Id).FirstOrDefault();
        }

    }
}
