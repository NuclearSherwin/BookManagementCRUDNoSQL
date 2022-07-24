using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Title")]
        public string Name { get; set; }

        [Required]
        public string Authors { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required, Range(1990, int.MaxValue), DisplayName("Date public")]
        public int Year { get; set; }

        [DisplayName("Summary")]
        public string Description { get; set; }

        [DisplayName("File")]
        public string DataFile { get; set; }

    }
}
