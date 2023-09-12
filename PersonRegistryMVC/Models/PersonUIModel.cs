using PersonRegistryLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonRegistryMVC.Models
{
    public class PersonUIModel
    {
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(0, 100)]
        public int? Age { get; set; }

        public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();

    }
}
