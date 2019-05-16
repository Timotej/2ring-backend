using System.Collections.Generic;
using System.Threading.Tasks;

namespace project
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task AddAsync(Employee employee);
        Task<Employee> FindByIdAsync(int id);
        Task<bool> Update(Employee employee);
        Task ArchiveAsync(Employee employee);
        void Remove(Employee employee);
    }
}