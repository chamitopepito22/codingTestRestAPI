using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRestAPI.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        //public DbSet<EmployeeLatestTemp> employeeLatestTemp { get; set; }
        public virtual DbSet<Employees> _Employees { get; set; }
        public virtual DbSet<TempLogs> _TempLogs { get; set; }

        public virtual DbSet<Customers> _Customers { get; set; }
        public virtual DbSet<CustomerBoxes> _CustomerBoxes { get; set; }
        //public virtual DbSet<CustomerBoxDetails> _CustomerBoxDetails { get; set; }
        public virtual DbSet<Facilities> _Facilities { get; set; }

        public virtual DbSet<CovidData> _CovidData { get; set; }

        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CustomerBoxes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("CustomerBoxes");


                entity.Property(e => e.CustomerId).IsRequired();

                entity.Property(e => e.FacilityId).IsRequired();


                entity.Property(e => e.BoxType).IsRequired();

                entity.Property(e => e.CreatedDate).IsRequired();

                entity.Property(e => e.Notes).IsRequired();
            });


            builder.Entity<Facilities>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Facilities");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.SmallCount).IsRequired();

                entity.Property(e => e.MediumCount).IsRequired();

                entity.Property(e => e.LargeCount).IsRequired();

                entity.Property(e => e.SmallMaxCount).IsRequired();

                entity.Property(e => e.MediumMaxCount).IsRequired();

                entity.Property(e => e.LargeMaxCount).IsRequired();
            });


            builder.Entity<TempLogs>(entity =>
            {
                entity.HasKey(e => e.TempLogId);

                entity.ToTable("TempLogs");

                entity.Property(e => e.Temperature).IsRequired();

                entity.Property(e => e.RecordDate).IsRequired();

                entity.Property(e => e.TempType).IsRequired();
            });


            builder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Employees");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();
            });



            builder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.ToTable("Customers");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();
            });

            builder.Entity<CovidData>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("CovidData");

                entity.Property(e => e.SNo).IsRequired();

                entity.Property(e => e.ObservationDate).IsRequired();

                entity.Property(e => e.ProvinceState).IsRequired();

                entity.Property(e => e.CountryRegion).IsRequired();

                entity.Property(e => e.LastUpdate).IsRequired();

                entity.Property(e => e.Confirmed).IsRequired();

                entity.Property(e => e.Deaths).IsRequired();

                entity.Property(e => e.Recovered).IsRequired();

            });

            builder.Query<CustomerBoxDetails>();



            builder.Query<Employee_Get>();


            // [Asma Khalid]: Regster store procedure custom object.
            //builder.Query<Employee>();
            //builder.Query<TemperatureLogs>();
        } 
        /// <summary>
        /// Get products whose price is greater than equal to 1000 store procedure method.
        /// </summary>
        /// <returns>Returns - List of products whose price is greater than equal to 1000</returns>
        public async Task<List<Employee_Get>> GetEmployeeLatestTemp(EmployeeFilter employeeFilter)
        {
            // Initialization.
            List<Employee_Get> lst = new List<Employee_Get>();
            try
            {
                // Processing.
                string sqlQuery = "EXEC [dbo].[Employee_Get] @EmployeeId={0}, @DateFrom={1}, @DateTo={2}, @FirstName={3}, @LastName={4}, @TemperatureFrom={5}, @TemperatureTo={6}";
                lst = await this.Query<Employee_Get>().FromSql(sqlQuery, employeeFilter.EmployeeId, employeeFilter.DateFrom, employeeFilter.DateTo, employeeFilter.FirstName, employeeFilter.LastName, employeeFilter.TemperatureFrom, employeeFilter.TemperatureTo).ToListAsync();
            }
            catch (Exception ex)
            {
                
            }
            // Info.
            return lst;
        }

        public async Task<List<CustomerBoxDetails>> GetCustomerBoxesDetails(int CustomerId, int FacilityId, int BoxType, string Notes)
        {
            // Initialization.
            List<CustomerBoxDetails> lst = new List<CustomerBoxDetails>();
            try
            {
                // Processing.
                string sqlQuery = "EXEC [dbo].[CustomerBoxes_Details_Get] @CustomerId={0}, @FacilityId={1}, @BoxType={2}, @Notes={3}";
                lst = await this.Query<CustomerBoxDetails>().FromSql(sqlQuery, CustomerId, FacilityId, BoxType, Notes).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            // Info.
            return lst;
        }

        public async Task<List<Customers_Get>> GetCustomersAllWithBoxCount()
        {
            // Initialization.
            List<Customers_Get> lst = new List<Customers_Get>();
            try
            {
                // Processing.
                string sqlQuery = "EXEC [dbo].[Customers_Get]";
                lst = await this.Query<Customers_Get>().FromSql(sqlQuery).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            // Info.
            return lst;
        }

        public bool CustomerBoxesRetrievePackage(CustomerBox customerBoxes)
        {
            try
            {
                if(customerBoxes.Id == -1 || customerBoxes.CustomerId <= 0 || customerBoxes.FacilityId <= 0) return false;
                string sqlQuery = "EXEC [dbo].[CustomerBoxes_RetrievePackage] @CustomerId = {0}, @FacilityId = {1},@BoxType = {2},@Id = {3}";
                var result = Database.ExecuteSqlCommand(sqlQuery, customerBoxes.CustomerId, customerBoxes.FacilityId, customerBoxes.BoxType, customerBoxes.Id);
                if (result == 3)
                    return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }


        public bool CustomerBoxesStorePackage(CustomerBox customerBoxes)
        {
            try
            {
                string sqlQuery = "EXEC [dbo].[CustomerBoxes_StorePackage] @CustomerId = {0}, @FacilityId = {1},@BoxType = {2}, @Notes = {3}";
                var result = Database.ExecuteSqlCommand(sqlQuery, customerBoxes.CustomerId, customerBoxes.FacilityId, customerBoxes.BoxType, customerBoxes.Notes);
                if (result == 3)
                    return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool Customer_Delete(int Id)
        {
            try
            {
                string sqlQuery = "exec [dbo].Customer_Delete @Id = {0} ";
                var result = Database.ExecuteSqlCommand(sqlQuery, Id);
                if (result == 3)
                    return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
