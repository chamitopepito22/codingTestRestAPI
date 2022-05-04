using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Models
{
    public class CRUD
    {
        public enum TargetTables
        {
            Employee = 1,
            TempLogs = 2,
        }

        
        public Employees employees { get; set; }
        public TempLogs templLogs { get; set; }


        DBContext _dbContext;
        public async Task<object> Create(TargetTables TargetTable)
        {
            if(TargetTable == TargetTables.Employee)
            {

                var _obj = await _dbContext._Employees.AddAsync(employees);
                _dbContext._Employees.Add(employees);
                _dbContext.SaveChanges();
                return _obj.Entity;
            }
            else
            {
                var _obj = await _dbContext._TempLogs.AddAsync(templLogs);
                _dbContext._TempLogs.Add(templLogs);
                _dbContext.SaveChanges();
                return _obj.Entity;
            }
        }

        public void Delete(TargetTables TargetTable)
        {
            if (TargetTable == TargetTables.Employee)
            {
                _dbContext.Remove(employees);
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.Remove(templLogs);
                _dbContext.SaveChanges();
            }
            
        }
        public bool Update(TargetTables TargetTable)
        {
            try
            {
                if (TargetTable == TargetTables.Employee)
                {
                    _dbContext._Employees.Update(employees);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    _dbContext._TempLogs.Update(templLogs);
                    _dbContext.SaveChanges();
                    return true;
                }

                
            }
            catch (Exception)
            {
                return false;
            }
        }

        

        
    }
}
