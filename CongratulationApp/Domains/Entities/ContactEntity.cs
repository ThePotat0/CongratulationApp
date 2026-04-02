using System.ComponentModel.DataAnnotations;

namespace CongratulationApp.Domains.Entities
{
    public class ContactEntity
    {
        public ICollection<ContactEntity>? Contacts { get; set; }
        public int Id { get; set; }
        [MaxLength(200)]
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please, enter the name!")]
        public string? Name { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Photo { get; set; }
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
    }
}
