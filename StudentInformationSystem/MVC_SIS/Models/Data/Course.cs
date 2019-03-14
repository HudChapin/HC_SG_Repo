using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required (ErrorMessage = "Please enter a course name.")]
        public string CourseName { get; set; }
    }
}