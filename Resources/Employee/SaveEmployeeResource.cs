using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public class SaveEmployeeResource : IValidatableObject
    {
        [Required, MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }


        [Required, MinLength(1), MaxLength(30)]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public decimal? Salary { get; set; }

        public List<SinglePositionDuration> PositionsDuration { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!IsBirthDateValid(DateOfBirth))
            {
                results.Add(new ValidationResult("Birth date must be less than today."));
            }
            if (Salary.HasValue && Salary.Value < 520)
            {
                results.Add(new ValidationResult("Salary must be at least 520€"));
            }
            return results;
        }

        bool IsBirthDateValid(DateTime? date)
        {
            if (date.HasValue)
                return date.Value.Date < DateTime.Now.Date;

            return false;
        }

        bool IsStartDateValid(DateTime? date)
        {
            if (date.HasValue)
                return date.Value.Date >= DateTime.Now.Date;

            return false;
        }
    }
}