using System;
using System.ComponentModel.DataAnnotations;
using Training.NETReact.Domain.ENUM;

namespace Training.NETReact.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
