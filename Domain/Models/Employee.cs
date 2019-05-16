using System;
using System.Collections.Generic;

namespace project
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Salary { get; set; }

        public bool Archived { get; set; }

        public List<SinglePositionDuration> PositionsDuration { get; set; }

    }
}