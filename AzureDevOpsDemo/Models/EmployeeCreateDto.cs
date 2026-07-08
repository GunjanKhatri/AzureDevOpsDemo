using System;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsDemo.Models
{
    public class EmployeeCreateDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [EmailAddress, MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }

        public DateTime? DateHired { get; set; }
    }
}