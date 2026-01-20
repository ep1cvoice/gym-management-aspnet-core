using System.ComponentModel.DataAnnotations;

namespace GymApp.Models.ViewModels
{
    public class EditAddressViewModel
    {
        [MaxLength(150)]
        public string? Street { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }
    }
}
