using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;

namespace project
{
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeResource>> GetAllAsync()
        {
            var employees = await _employeeService.ListAsync();
            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);
            return mappedEmployees;
        }

        [HttpGet("{id}")]
        public async Task<EmployeeResource> GetEmployeeByIdAsync(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            var mappedEmp = _mapper.Map<Employee, EmployeeResource>(emp);
            return mappedEmp;
        }

        [HttpGet("active")]
        public async Task<IEnumerable<EmployeeResource>> ListActiveAsync()
        {
            var emp = await _employeeService.ListActiveAsync();
            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(emp);
            return mappedEmp;
        }

        [HttpGet("archived")]
        public async Task<IEnumerable<EmployeeResource>> ListArchivedAsync()
        {
            var emp = await _employeeService.ListArchivedAsync();
            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(emp);
            return mappedEmp;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);
            var result = await _employeeService.SaveAsync(employee);

            if (!result.Success)
                return BadRequest(result.Message);

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Employee);
            return Ok(employeeResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);
            var result = await _employeeService.UpdateAsync(id, employee);

            if (!result.Success)
                return BadRequest(result.Message);

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Employee);
            return Ok(employeeResource);
        }

        [HttpPut("archive/{id}")]
        public async Task<IActionResult> ArchiveEmployee(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _employeeService.ArchiveEmployeeAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Employee);
            return Ok(employeeResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Employee);
            return Ok(employeeResource);
        }
    }
}