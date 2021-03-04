using System;
using System.ComponentModel.DataAnnotations;

namespace MYMA.Models
{
    public class Student
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UrduName { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        [Required]
        public DateTime AdmisstionDate { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string CurrentClassId { get; set; }
    }
}