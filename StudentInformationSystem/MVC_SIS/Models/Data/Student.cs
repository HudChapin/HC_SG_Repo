using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Student : IValidatableObject
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please enter the students first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the students last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a GPA")]
        public decimal GPA { get; set; }
        public Address Address { get; set; }
        public Major Major { get; set; }
        public List<Course> Courses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (GPA < 0.0M || GPA > 4.0M)
            {

                errors.Add(new ValidationResult("The students GPA must be 0.0 - 4.0", new[] { "GPA" }));
            }

            return errors;
        }
    }
}