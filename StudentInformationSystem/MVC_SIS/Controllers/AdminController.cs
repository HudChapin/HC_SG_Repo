using Exercises.Models.Data;
using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddState()
        {
            return View(new State());
        }

        [HttpPost]
        public ActionResult AddState(State model)
        {
            if (string.IsNullOrEmpty(model.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "Please enter the state abbreviation.");
            }

            if (!string.IsNullOrEmpty(model.StateAbbreviation))
            {
                if (model.StateAbbreviation.Length > 2 || model.StateAbbreviation.Length < 2)
                {
                    ModelState.AddModelError("StateAbbreviation", "The state abbreviation must be 2 characters.");
                }
            }

            if (string.IsNullOrEmpty(model.StateName))
            {
                ModelState.AddModelError("StateName", "Please enter the state name.");
            }

            if (ModelState.IsValid)
            {
                StateRepository.Add(model);
                return RedirectToAction("States");
            }
                return View(model);
        }

        [HttpGet]
        public ActionResult EditState(string stateAbbreviation)
        {
            var state = StateRepository.Get(stateAbbreviation);
            return View(state);
        }

        [HttpPost]
        public ActionResult EditState(State model)
        {
            if (string.IsNullOrEmpty(model.StateName))
            {
                ModelState.AddModelError("StateName", "Please enter the state name.");
            }

            if (!string.IsNullOrEmpty(model.StateAbbreviation))
            {
                if (model.StateAbbreviation.Length > 2 || model.StateAbbreviation.Length < 2)
                {
                    ModelState.AddModelError("StateAbbreviation", "The state abbreviation should only be 2 characters");
                }
            }

            if (ModelState.IsValid)
            {
                StateRepository.Edit(model);
                return RedirectToAction("States");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteState(string stateAbbreviation)
        {
            var state = StateRepository.Get(stateAbbreviation);
            return View(state);
        }

        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpPost]
        public ActionResult AddMajor(Major model)
        {
            if (string.IsNullOrEmpty(model.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter the major name.");
            }

            if (ModelState.IsValid)
            {
                MajorRepository.Add(model.MajorName);
                return RedirectToAction("Majors");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }


        [HttpPost]
        public ActionResult EditMajor(Major model)
        {
            if (string.IsNullOrEmpty(model.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please enter the major name.");
            }

            if (ModelState.IsValid)
            {
                MajorRepository.Edit(model);
                return RedirectToAction("Majors");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult Courses()
        {
            var model = CourseRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new Course());
        }

        [HttpPost]
        public ActionResult AddCourse(Course model)
        {
            if (string.IsNullOrEmpty(model.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter the course name.");
            }

            if (ModelState.IsValid)
            {
                CourseRepository.Add(model.CourseName);
                return RedirectToAction("Courses");
            }

            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course model)
        {
            if (string.IsNullOrEmpty(model.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter the course name.");
            }

            if (ModelState.IsValid)
            {
                CourseRepository.Edit(model);
                return RedirectToAction("Courses");
            }

            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult DeleteCourse(Course course)
        {
            CourseRepository.Delete(course.CourseId);
            return RedirectToAction("Courses");
        }
    }
}