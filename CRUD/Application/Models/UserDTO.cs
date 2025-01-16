using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        // UserId should not be null and should have a maximum length of 50 characters
        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(50, ErrorMessage = "User ID cannot be longer than 50 characters.")]
        public string? UserId { get; set; }

        // FirstName is required and should not exceed 100 characters
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
        public string? FirstName { get; set; }

        // LastName is required and should not exceed 100 characters
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        public string? LastName { get; set; }

        // Email should be in a valid email format and required
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(200, ErrorMessage = "Email cannot be longer than 200 characters.")]
        public string? Email { get; set; }

        // MobileNo should have a valid phone number format (optional, but if provided, must match the regex)
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Mobile Number cannot be longer than 15 digits.")]
        public string? MobileNo { get; set; }

        // CreateId is optional, but if present, should be a valid integer
        public int? CreateId { get; set; }

        // CreateDate is optional, but if present, should be a valid date
        public DateTime? CreateDate { get; set; }

        // UpdateId is optional, but if present, should be a valid integer
        public int? UpdateId { get; set; }

        // UpdateDate is optional, but if present, should be a valid date
        public DateTime? UpdateDate { get; set; }
    }
}
