using Microsoft.AspNetCore.Mvc;
using thpt.ThachBan.DAL;
using thpt.ThachBan.DTO.Models;
using thpt.ThachBan.DTO.ViewModels.Areas.Admin;

namespace thpt.ThachBan.v2.Areas.Admin.Controllers
{
    public class ClassManagerController : Controller
    {

        public IActionResult Index(
            int Status = 1, int OrderBy = 0, int pageCurrent = 1, int size = 10, 
            string ClassNameSearch = null, 
            string NumOfMemSearch = null, 
            string NumOfSeatSearch = null,
            string GradeSearch =null,
            string EmployeeSearch =null
            )
        {
            ViewBag.ClassNameSearch=ClassNameSearch ;
            ViewBag.NumOfMemSearch=NumOfMemSearch ;
            ViewBag.NumOfSeatSearch=NumOfSeatSearch ;
            ViewBag.GradeSearch=GradeSearch ;
            ViewBag.EmployeeSearch = EmployeeSearch;
            ViewBag.Status = Status;
            List<AboutClass> aboutClasses = new List<AboutClass>();
            List<Class> classes;
            if (Status== 1)
            {
                classes = DatabaseContext.GetDB.Class.Where(x=>x.NumOfSeat- x.NumOfMem > 0).ToList();
            }
            else
            {
                classes = DatabaseContext.GetDB.Class.Where(x => x.NumOfSeat - x.NumOfMem <= 0).ToList();
            }
            if (!String.IsNullOrEmpty(ClassNameSearch))
            {
                classes = classes.Where(x => x.ClassName.Contains(ClassNameSearch)).ToList();
            }
            if (!String.IsNullOrEmpty(NumOfMemSearch))
            {
                classes = classes.Where(x => x.NumOfMem.ToString()==NumOfMemSearch).ToList();
            }
            if (!String.IsNullOrEmpty(NumOfSeatSearch))
            {
                classes = classes.Where(x => x.NumOfSeat.ToString() == NumOfSeatSearch).ToList();
            }
            if (!String.IsNullOrEmpty(GradeSearch))
            {
                classes = classes.Where(x => x.Grade.ToString()==GradeSearch).ToList();
            }
            if (!String.IsNullOrEmpty(EmployeeSearch))
            {
                var eNames = DatabaseContext.GetDB.Employee.Where(x => x.EmployeeName.Contains(EmployeeSearch)).Select(x=>x.EmployeeId)?.ToList();
                classes = (from e in eNames
                           join c in DatabaseContext.GetDB.Class
                           on e equals c.EmployeeId
                           select c).ToList();
            }
            for (int i = 0; i < classes.Count; i++)
            {
                AboutClass aboutClass = new AboutClass();
                aboutClass._Class = classes[i];
                aboutClass.EmployeeName = DatabaseContext.GetDB.Employee.Find(classes[i].EmployeeId)?.EmployeeName;
                aboutClasses.Add( aboutClass );
            }
            return View(aboutClasses);
        }
    }
}
