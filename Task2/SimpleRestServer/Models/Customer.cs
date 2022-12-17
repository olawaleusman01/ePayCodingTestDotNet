using System.ComponentModel.DataAnnotations;

namespace SimpleRestServer.Models
{
    public class Customer
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [Range(18, 150, ErrorMessage = "Age must be greater than 18")]
        public int Age { get; set; }
        [Required]
        public int Id { get; set; }

    }
}