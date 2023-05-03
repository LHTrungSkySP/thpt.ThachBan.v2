using Microsoft.AspNetCore.Mvc;
using thpt.ThachBan.DAL;
using thpt.ThachBan.DTO.Models;
using thpt.ThachBan.DTO.ViewModels.Areas.Admin;

namespace thpt.ThachBan.v2.Areas.Admin.Controllers
{
    public class SubjectManagerController : Controller
    {
        public IActionResult Index(
            int OrderBy = 0, int pageCurrent = 1, int size = 10,
            string SubjectNameSearch = null,
            string MaxLessonADaySearch = null,
            string LessonAWeek = null,
            string DepartmentSearch = null
            )
        {
            ViewBag.SubjectNameSearch = SubjectNameSearch ;
            ViewBag.MaxLessonADaySearch = MaxLessonADaySearch ;
            ViewBag.LessonAWeek = LessonAWeek ;
            ViewBag.DepartmentSearch = DepartmentSearch;
            List<Subject> subjects = DatabaseContext.GetDB.Subject.ToList();
            foreach( Subject subject in subjects )
            {
                subject.Department=DatabaseContext.GetDB.Department.Find(subject.DepartmentId);
            }
            if (!String.IsNullOrEmpty(SubjectNameSearch))
            {
                subjects = subjects.Where(x => x.SubjectName.Contains(SubjectNameSearch)).ToList();
            }
            if (!String.IsNullOrEmpty(MaxLessonADaySearch))
            {
                subjects = subjects.Where(x => x.MaxLessonAday.ToString() == MaxLessonADaySearch).ToList();
            }
            if (!String.IsNullOrEmpty(LessonAWeek))
            {
                subjects = subjects.Where(x => x.LessonAweek.ToString() == LessonAWeek).ToList();
            }
            if (!String.IsNullOrEmpty(DepartmentSearch))
            {
                subjects = subjects.Where(x => x.Department.DepartmentName.Contains(DepartmentSearch)).ToList();
            }
            return View(subjects);
        }
    }
}
