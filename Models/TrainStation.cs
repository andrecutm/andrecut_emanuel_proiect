using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndrecutEmanuelMedii.Models
{
    public class TrainStation
    {
        public int ID { get; set; }

        [Required, StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }
        public ICollection<Train>? Trains { get; set; }
        public ICollection<Train>? DestinationTrains { get; set; }
    }
}
