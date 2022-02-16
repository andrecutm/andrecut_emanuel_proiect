using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndrecutEmanuelMedii.Models
{
    public class Facility
    {
        public int ID { get; set; }

        [Required, StringLength(255, MinimumLength = 1)]
        [Display(Name = "Facility name")]
        public string Name { get; set; }

        public ICollection<Wagon>? Wagons { get; set; }
    }
}
