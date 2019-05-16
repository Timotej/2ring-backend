using System;

namespace project
{
    public class EmployeeResponse : BaseResponse
    {
        public Employee Employee { get; private set; }

        private EmployeeResponse(bool success, string message, Employee employee) : base(success, message)
        {
            Employee = employee;
        }
        public EmployeeResponse(Employee employee) : this(true, string.Empty, employee)
        { }

        public EmployeeResponse(string message) : this(false, message, null)
        { }
    }
}