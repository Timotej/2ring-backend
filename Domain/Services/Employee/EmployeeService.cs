using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace project
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _employeeRepository.ListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> ListActiveAsync()
        {
            var emps = await _employeeRepository.ListAsync();
            return emps.Where(x => !x.Archived);
        }

        public async Task<IEnumerable<Employee>> ListArchivedAsync()
        {
            var emps = await _employeeRepository.ListAsync();
            return emps.Where(x => x.Archived);
        }

        public async Task<EmployeeResponse> SaveAsync(Employee employee)
        {
            try
            {
                await _employeeRepository.AddAsync(employee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when saving the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Address = employee.Address;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.PositionsDuration = employee.PositionsDuration;
            Console.WriteLine("TUNAK" + existingEmployee.PositionsDuration?.Count);
            try
            {
                var success = await _employeeRepository.Update(existingEmployee);
                if (success)
                {
                    await _unitOfWork.CompleteAsync();

                    return new EmployeeResponse(existingEmployee);
                }
                else
                {
                    return new EmployeeResponse("Start date of new position must be later then start date of current position");
                }
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when updating the employee: {ex.Message}");
            }

        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            try
            {
                _employeeRepository.Remove(existingEmployee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when deleting the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> ArchiveEmployeeAsync(int id)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            try
            {
                await _employeeRepository.ArchiveAsync(existingEmployee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when archiving the employee: {ex.Message}");
            }
        }
    }
}