using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public int BookID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]
        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }
        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$", ErrorMessage = "Numele autorului trebuie sa fie de forma 'Prenume Nume'"), Required, StringLength(50, MinimumLength = 3)]
        public string Author { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime PublishingDate { get; set; }

        [Range(1, 300)]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(6, 2)")] //permite valori cu doua zecimale
        public decimal Price { get; set; }

       
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; } //navigation property

        [Display(Name = "Categories")]
        public ICollection<BookCategory> BookCategories { get; set; }

    }
}
