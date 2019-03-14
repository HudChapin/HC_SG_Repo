using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());
            
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            if (string.IsNullOrEmpty(studentVM.Student.FirstName))
            {
                ModelState.AddModelError("FistName", "Please enter the students first name.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter the students last name.");
            }

            if (studentVM.Student.GPA > 4.0M || studentVM.Student.GPA < 0.0M)
            {
                ModelState.AddModelError("GPA", "Please enter the GPA between 0.0 and 4.0.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.Street1))
            {
                ModelState.AddModelError("Student.Address.Street1", "You must provide atleast one street address.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.City))
            {
                ModelState.AddModelError("Student.Address.City", "Please enter the city.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.PostalCode))
            {
                ModelState.AddModelError("Student.Address.PostalCode", "Please enter the postal code.");
            }

            if (ModelState.IsValid)
            {
                StudentRepository.Add(studentVM.Student);
                return RedirectToAction("List");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(id);
            viewModel.SelectedCourseIds = viewModel.Student.Courses.Select(s => s.CourseId).ToList();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Edit(StudentVM studentVM)
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());

            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            StudentRepository.Edit(studentVM.Student);

            StudentRepository.SaveAddress(studentVM.Student.StudentId, studentVM.Student.Address);

            if (string.IsNullOrEmpty(studentVM.Student.FirstName))
            {
                ModelState.AddModelError("FistName", "Please enter the students first name.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter the students last name.");
            }

            if (studentVM.Student.GPA > 4.0M || studentVM.Student.GPA < 0.0M)
            {
                ModelState.AddModelError("GPA", "Please enter the GPA between 0.0 and 4.0.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.Street1))
            {
                ModelState.AddModelError("Student.Address.Street1", "You must provide atleast one street address.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.City))
            {
                ModelState.AddModelError("Student.Address.City", "Please enter the city.");
            }

            if (string.IsNullOrEmpty(studentVM.Student.Address.PostalCode))
            {
                ModelState.AddModelError("Student.Address.PostalCode", "Please enter the postal code.");
            }
            
            if (ModelState.IsValid)
            {
                StudentRepository.Edit(studentVM.Student);
                return RedirectToAction("List");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int studentId)
        {
            var student = StudentRepository.Get(studentId);
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}