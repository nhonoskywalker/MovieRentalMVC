using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Models
{
    public class MovieModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsRented { get; set; }

        public DateTime RentalDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
