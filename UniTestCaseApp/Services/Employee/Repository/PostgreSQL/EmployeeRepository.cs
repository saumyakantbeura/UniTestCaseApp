using Microsoft.EntityFrameworkCore;
using UniTestCaseApp.Data;

namespace UniTestCaseApp.Services.Employee.Repository.PostgreSQL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dataContext;

        public EmployeeRepository(AppDbContext dataContext)
        { 
            _dataContext = dataContext;
        }
        public async Task<UniTestCaseApp.Services.Employee.Domain.Employee> AddEmployee(UniTestCaseApp.Services.Employee.Domain.Employee employee)
        {
            _dataContext.Employees.Add(employee);
            await _dataContext.SaveChangesAsync();
            return employee;
        }

        public async Task<List<UniTestCaseApp.Services.Employee.Domain.Employee>> GetAllEmployees()
        {
            // Retrieve all employees from the database
            var employees = await _dataContext.Employees.ToListAsync();

            // Return the list of employees
            return employees;
        }

        public async Task<UniTestCaseApp.Services.Employee.Domain.Employee> GetEmployee(int id)
        {
            var getEmployeeList= await _dataContext.Employees.FindAsync(id);
            return getEmployeeList;
        }

        public async Task<int> DeleteEmployee(int id)
        {
            // Find the employee by id
            var employee = await _dataContext.Employees.FindAsync(id);
            int rowsAffected = 0;
            // If the employee doesn't exist, you can handle the case appropriately
            if (employee != null)
            {


                // Remove the employee from the DbSet
                _dataContext.Employees.Remove(employee);

                // Save the changes to commit the deletion
                 rowsAffected = await _dataContext.SaveChangesAsync();

                // Return the number of rows affected (1 if successful, 0 if no change)
               
            }
            return rowsAffected;
        }

        public async Task<int> DeleteAllEmployees()
        {
            // Retrieve all employees from the DbSet
            var allEmployees = _dataContext.Employees.ToList();

            int rowsAffected = 0;

            // Check if there are any employees in the table
            if (allEmployees.Any())
            {
                // Remove all employees from the DbSet
                _dataContext.Employees.RemoveRange(allEmployees);

                // Save the changes to commit the deletion
                rowsAffected = await _dataContext.SaveChangesAsync();
            }

            // Return the number of rows affected (number of employees deleted)
            return rowsAffected;
        }

        public async Task<int> DeleteAllEmployeesRaw()
        {
            // Execute raw SQL to delete all employees from the Employees table
            var rowsAffected = await _dataContext.Database.ExecuteSqlRawAsync("DELETE FROM Employees");

            // Return the number of rows affected
            return rowsAffected;
        }


        public async Task<int> TruncateAllEmployees()
        {
            // Execute raw SQL to truncate the Employees table
            var rowsAffected = await _dataContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Employees");

            // Return the number of rows affected (for TRUNCATE, it's usually 0 because it does not return a count)
            return rowsAffected;
        }


    }
}
