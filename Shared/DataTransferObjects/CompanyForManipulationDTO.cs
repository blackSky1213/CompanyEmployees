using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record CompanyForManipulationDTO
    {

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters.")]
        public string? Address { get; set; }
        public string? Country { get; set; }
        public ICollection<EmployeeDTO>? Employees { get; set; }
    }
}
