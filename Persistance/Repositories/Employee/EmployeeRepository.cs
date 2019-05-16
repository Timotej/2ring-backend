using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace project
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {

            var positions = await _context.PositionsDuration.ToListAsync();
            var employees = await _context.Employees.ToListAsync();

            foreach (var item in employees)
            {
                item.PositionsDuration = positions.Where(x => x.EmployeeId == item.Id).ToList();
            }

            //var employees = await _context.Employees.Include(x => x.PositionsDuration).ToListAsync();
            return employees;
        }

        public async Task AddAsync(Employee employee)
        {
            foreach (var item in employee.PositionsDuration)
            {
                item.EmployeeId = employee.Id;
            }
            await _context.Employees.AddAsync(employee);
        }
        public async Task<Employee> FindByIdAsync(int id)
        {
            //var positions = await _context.PositionsDuration.ToListAsync();

            var employee = await _context.Employees.FindAsync(id);
            //employee.PositionsDuration = positions.Where(x => x.EmployeeId == id).ToList();
            employee.PositionsDuration = _context.PositionsDuration.Where(x => x.EmployeeId == id).ToList();
            return employee;
        }
        public async Task<bool> Update(Employee employee)
        {
            var positions = await _context.PositionsDuration.ToListAsync();
            positions = positions.Where(x => x.EmployeeId == employee.Id).ToList();
            if (employee.PositionsDuration != null && employee.PositionsDuration.Count == 1)
            {
                var pos = employee.PositionsDuration[0];

                foreach (var item in positions)
                {
                    employee.PositionsDuration.Add(item);
                    if (!item.EndDate.HasValue)
                    {
                        if (item.StartDate >= pos.StartDate) return false;
                        item.EndDate = pos.StartDate;
                    }
                }
            }
            else
            {
                if (employee.PositionsDuration == null) employee.PositionsDuration = new List<SinglePositionDuration>();
                employee.PositionsDuration.AddRange(positions);
            }

            _context.Employees.Update(employee);
            return true;
        }

        public async Task ArchiveAsync(Employee employee)
        {
            var positions = await _context.PositionsDuration.ToListAsync();
            positions.Where(x => x.EmployeeId == employee.Id);

            foreach (var item in positions)
            {
                if (!item.EndDate.HasValue)
                {
                    item.EndDate = DateTime.Now.Date;
                    _context.PositionsDuration.Update(item);
                }
            }
            employee.Archived = true;
            _context.Employees.Update(employee);
        }
        public void Remove(Employee employee)
        {
            // var posDurations = await _context.PositionsDuration.ToListAsync();
            //var employeePositions = posDurations.Where(x => x.EmployeeId == employee.Id);
            //foreach (var item in employeePositions) _context.PositionsDuration.Remove(item);

            _context.Employees.Remove(employee);
        }

    }
}