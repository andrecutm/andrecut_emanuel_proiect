using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndrecutEmanuelMedii.Models
{
    public class Train
    {
        public int ID { get; set; }

        [Required, StringLength(255, MinimumLength = 2)]
        [Display(Name = "Operating Company")]
        public string OperatingCompany { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DepartureDateTime { get; set; }

        [Display(Name = "Train Station")]
        public int TrainStationID { get; set; }
        [Display(Name = "Train Station")]
        public TrainStation? TrainStation { get; set; }

        [Display(Name = "Destination Train Station")]
        public int? DestinationTrainStationID { get; set; }
        [ForeignKey("DestinationTrainStationID")]
        [InverseProperty("DestinationTrains")]
        [Display(Name = "Destination Train Station")]
        public TrainStation? DestinationTrainStation { get; set; }

        public ICollection<Wagon>? Wagons { get; set; }

    }
}  
