using System.Collections.Generic;
using System.Threading.Tasks;

namespace project
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> ListActiveAsync();
        Task<IEnumerable<Employee>> ListArchivedAsync();
        Task<EmployeeResponse> SaveAsync(Employee position);
        Task<EmployeeResponse> UpdateAsync(int id, Employee category);
        Task<EmployeeResponse> DeleteAsync(int id);
        Task<EmployeeResponse> ArchiveEmployeeAsync(int id);
    }
}