using System;
using System.ComponentModel.DataAnnotations;

namespace project
{
    public class SaveSinglePositionDurationResource
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}