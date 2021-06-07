using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleUniversityApp
{
    internal class StudentModel
    {
        public int StudentId { get; set; }

        [Required (ErrorMessage = "First Name cannot be empty!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid length! First Name must have more than 2 letters.")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "First Name must contain only letters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid length! Last Name must have more than 2 letters.")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Last Name must contain only letters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number cannot be empty")]
        [RegularExpression(@"^\+[0-9]+$", ErrorMessage = "The phone number must be in the format +375xxxxxxxxx")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birth Date cannot be empty")]
        [RegularExpression(@"^[0-9]\d{4}-\d{2}-\d{2}$", ErrorMessage = "The Birth Date must be in the format yyyy-mm-dd")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress (ErrorMessage = "Email must be in the format xxxx@xxx.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address { get; set; }
    }
}
