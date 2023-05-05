using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using thpt.ThachBan.DAL;
using thpt.ThachBan.DTO.Models;
using thpt.ThachBan.DTO.ViewModels.Areas.Admin;

namespace thpt.ThachBan.v2.Areas.Admin.Controllers
{
    public class ScheduleManagerController : Controller
    {
        [HttpGet]
        public IActionResult Create(Guid id)
        {
            TempData["ClassId"] = id;
            return View();
        }
        
        [HttpPost]
        public IActionResult loadSubjectOfClass([FromBody] loadSubjectOfClassPost loadSubjectOfClassPost)
        {
            List<SubjectAvaiableCreateView> subjectsView = new List<SubjectAvaiableCreateView>();
            List<Subject> subjects = DatabaseContext.GetDB.Subject.Where(x=>x.SubjectName.Contains(loadSubjectOfClassPost.grade.ToString())).ToList();

            for (int i = 0; i < subjects.Count; i++)
            {
                int sum = 0;
                foreach (Guid id in loadSubjectOfClassPost.guids)
                {
                    if (subjects[i].SubjectId == id)
                    {
                        sum++;
                    }
                }
                if (subjects[i].LessonAweek > sum)
                {
                    SubjectAvaiableCreateView subjectView = new SubjectAvaiableCreateView();
                    subjectView.subject = subjects[i];
                    subjectView.inventory = subjects[i].LessonAweek - sum;
                    subjectsView.Add(subjectView);
                }
            }
            subjectsView = subjectsView.OrderBy(x => x.subject.SubjectName).ToList();
            return Json(subjectsView.Select(x => new
            {
                id = x.subject.SubjectId, 
                name = x.subject.SubjectName,
                sl=x.inventory
            }).ToList());
        }
        [HttpPost]
        public IActionResult CreateSchedule([FromBody] CreateSchedulePost createSchedulePost)
        {
            for (int i = 0; i < 5; i++)//tiết
            {
                for (int j = 0; j < 6; j++)//ngày
                {
                    Schedule schedule = new Schedule();
                    schedule.ClassId = createSchedulePost.ClassId;
                    schedule.ClassTime = i;
                    schedule.Day = j;
                    if (i == 0) // tiết 1
                    {
                        schedule.SubjectId = createSchedulePost.tiet1[j];
                    }
                    else if (i == 1)
                    {
                        schedule.SubjectId = createSchedulePost.tiet2[j];
                    }
                    else if (i == 2)
                    {
                        schedule.SubjectId = createSchedulePost.tiet3[j];
                    }
                    else if (i == 3)
                    {
                        schedule.SubjectId = createSchedulePost.tiet4[j];
                    }
                    else if (i == 4)
                    {
                        schedule.SubjectId = createSchedulePost.tiet5[j];
                    }
                    DatabaseContext.GetDB.Schedule.Add(schedule);
                    DatabaseContext.GetDB.SaveChanges();
                }
            }
            return Json(new
            {
                status = 200,
            });
        }
    }
}
