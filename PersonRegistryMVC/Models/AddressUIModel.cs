using System.ComponentModel.DataAnnotations;

namespace PersonRegistryMVC.Models
{
    public class AddressUIModel
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int PersonId { get; set; }
    }
}
