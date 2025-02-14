using UniTestCaseApp.Services.Employee.Domain;

namespace UniTestCaseApp.Services.Employee.Repository.PostgreSQL
{
    public interface IEmployeeRepository 
    {
        public Task<UniTestCaseApp.Services.Employee.Domain.Employee> AddEmployee(UniTestCaseApp.Services.Employee.Domain.Employee employee);

        public Task<List<UniTestCaseApp.Services.Employee.Domain.Employee>> GetAllEmployees();
        public Task<UniTestCaseApp.Services.Employee.Domain.Employee> GetEmployee(int id);
        //public Task<List<Model.Request.Employee>> GetEmployees();
        //public Task<int> UpdateEmployee(int id, Model.Request.Employee employee);
        public Task<int> DeleteEmployee(int id);

        public Task<int> DeleteAllEmployees();
        public Task<int> DeleteAllEmployeesRaw();
        public Task<int> TruncateAllEmployees();
    }
}
