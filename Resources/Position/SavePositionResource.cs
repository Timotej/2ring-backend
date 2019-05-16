using System.ComponentModel.DataAnnotations;

namespace project
{
    public class SavePositionResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public bool CanBeDeleted { get; set; }
    }
}