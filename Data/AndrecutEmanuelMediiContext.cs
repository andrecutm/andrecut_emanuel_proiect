#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndrecutEmanuelMedii.Models;

namespace AndrecutEmanuelMedii.Data
{
    public class AndrecutEmanuelMediiContext : DbContext
    {
        public AndrecutEmanuelMediiContext (DbContextOptions<AndrecutEmanuelMediiContext> options)
            : base(options)
        {
        }

        public DbSet<AndrecutEmanuelMedii.Models.Train> Train { get; set; }

        public DbSet<AndrecutEmanuelMedii.Models.TrainStation> TrainStation { get; set; }

        public DbSet<AndrecutEmanuelMedii.Models.Facility> Facility { get; set; }

        public DbSet<AndrecutEmanuelMedii.Models.Wagon> Wagon { get; set; }
    }
}
