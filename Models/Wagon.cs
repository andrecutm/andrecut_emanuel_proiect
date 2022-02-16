using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndrecutEmanuelMedii.Models
{
    public class Wagon
    {
        public int ID { get; set; }

        [Display(Name = "Total Seats")]
        [Column(TypeName = "decimal(3, 0)")]
        [Range(1, 999)]
        public int TotalSeats { get; set; }

        [Display(Name = "Free Seats")]
        [Column(TypeName = "decimal(3, 0)")]
        [Range(1, 999)]
        public int FreeSeats { get; set; }

        public ICollection<Facility>? Facilities { get; set; }

        public int TrainId { get; set; }
        public Train? Train { get; set; }
    }
}
