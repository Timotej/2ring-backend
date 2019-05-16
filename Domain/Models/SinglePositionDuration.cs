using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace project
{
    public class SinglePositionDuration
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
    }
}