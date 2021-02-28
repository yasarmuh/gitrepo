using System;

namespace MYMA.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UrduName { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime AdmisstionDate { get; set; }
        public string MobileNumber { get; set; }
        public string CurrentClassId { get; set; }
    }
}