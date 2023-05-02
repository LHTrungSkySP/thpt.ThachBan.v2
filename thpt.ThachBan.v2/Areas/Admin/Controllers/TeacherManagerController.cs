using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using thpt.ThachBan.BAL.ClassBAL;
using thpt.ThachBan.BAL.DepartmentBAL;
using thpt.ThachBan.BAL.DepartmentManagerBAL;
using thpt.ThachBan.BAL.EmployeeBAL;
using thpt.ThachBan.BAL.SubjectBAL;
using thpt.ThachBan.DAL;
using thpt.ThachBan.DTO.Models;
using thpt.ThachBan.DTO.ViewModels.Areas.Admin;
using thpt.ThachBan.DTO.ViewModels.Areas.Common;
using thpt.ThachBan.UI.Areas.Admin.Models;

namespace thpt.ThachBan.v2.Areas.Admin.Controllers
{
    public class TeacherManagerController : Controller
    {
        #region field
        private IEmployeeBAL employeeBAL;
        private IClassBAL classBAL;
        private ISubjectBAL subjectBAL;
        private IDepartmentBAL departmentBAL;
        private IDepartmentManagerBAL departmentManagerBAL;
        #endregion

        #region contructor
        public TeacherManagerController(IDepartmentManagerBAL departmentManagerBAL, IEmployeeBAL employeeBAL, IClassBAL classBAL, ISubjectBAL subjectBAL, IDepartmentBAL departmentBAL)
        {
            this.departmentManagerBAL = departmentManagerBAL;
            this.employeeBAL = employeeBAL;
            this.classBAL = classBAL;
            this.subjectBAL = subjectBAL;
            this.departmentBAL = departmentBAL;
        }
        #endregion
        [HttpGet]
        public IActionResult Index(int Status=1,int OrderBy=0,int pageCurrent = 1, int size = 10, string CodeSearch = null, string NameSearch = null, string ClassSearch = null, string SubjectSearch = null,string NumberPhoneSearch=null)
        {
            ViewBag.OrderBy = OrderBy;
            ViewBag.PageCurrent = pageCurrent;
            ViewBag.Size = size;
            ViewBag.CodeSearch = CodeSearch;
            ViewBag.NameSearch = NameSearch;
            ViewBag.ClassSearch = ClassSearch;
            ViewBag.SubjectSearch = SubjectSearch;
            ViewBag.NumberPhoneSearch = NumberPhoneSearch;
            ViewBag.Status = Status;
            List<AboutEmployeePage> aboutEmployees = employeeBAL.EmployeePaging(Status, CodeSearch, NameSearch, ClassSearch, SubjectSearch, NumberPhoneSearch);
            if (OrderBy == 0)
            {
                aboutEmployees=aboutEmployees.OrderBy(x => x.Employee.EmployeeCode).ToList();
            }
            else if(OrderBy == 1)
            {
                aboutEmployees=aboutEmployees.OrderBy(x => x.Employee.CreatedDate).ToList();
            }
            else if(OrderBy == 2)
            {
                aboutEmployees=aboutEmployees.OrderBy(x => x.Employee.EmployeeName).ToList();
            }
            ViewBag.pageSize = (int)Math.Ceiling((double)aboutEmployees.Count / size);
            aboutEmployees = aboutEmployees.Skip((pageCurrent - 1) * size).Take(size).ToList();
            ViewBag.pageCurrent = pageCurrent;
            return View(aboutEmployees);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Employee employee = DatabaseContext.GetDB.Employee.Find(id);
            TempData["Classes"] = DatabaseContext.GetDB.Class.Where(x => x.EmployeeId == null || x.EmployeeId == employee.EmployeeId).OrderBy(x => x.ClassName).ToList();

            TempData["Departments"] = DatabaseContext.GetDB.Department.OrderBy(x => x.DepartmentName).ToList();

            TempData["Subjects"] = DatabaseContext.GetDB.Subject.OrderBy(x => x.SubjectName).ToList();
            ViewBag.CreatedBy = DatabaseContext.GetDB.Account.Find(employee.CreatedBy).AccountCode;
            ViewBag.UpdatedBy = employee.UpdatedBy == null ? null: DatabaseContext.GetDB.Account.Find(employee.UpdatedBy).AccountCode;

            return View(employee);
        }

        public IActionResult loadDepartmentSelected(Guid employeeId)
        {
            var Departments = (from departMng in DatabaseContext.GetDB.DepartmentManager.Where(x => x.EmployeeId == employeeId)
                               join d in DatabaseContext.GetDB.Department
                               on departMng.DepartmentId equals d.DepartmentId
                               select d).ToList();
            List<ComboboxSelected> departmentSelecteds = new List<ComboboxSelected>();
            var ds_depart = DatabaseContext.GetDB.Department.OrderBy(x => x.DepartmentName).ToList();
            for (int i = 0; i < Departments.Count; i++)
            {
                ComboboxSelected departmentSelected = new ComboboxSelected();
                departmentSelected.stt = ds_depart.IndexOf(Departments[i]);
                departmentSelected.name = Departments[i].DepartmentName;
                departmentSelecteds.Add(departmentSelected);
            }
            return Json(departmentSelecteds.Select(d => new
            {
                stt = d.stt,
                name = d.name
            }).ToList());
        }
        public IActionResult loadSubjectSelected(Guid employeeId)
        {
            
            var Subjects = DatabaseContext.GetDB.Employee.Include(e => e.Subject).FirstOrDefault(x => x.EmployeeId == employeeId).Subject.ToList();
            List<ComboboxSelected> subjecttSelecteds = new List<ComboboxSelected>();
            var ds_Subjects = DatabaseContext.GetDB.Subject.OrderBy(x => x.SubjectName).ToList();
            for (int i = 0; i < Subjects.Count; i++)
            {
                ComboboxSelected subjecttSelected = new ComboboxSelected();
                subjecttSelected.stt = ds_Subjects.IndexOf(Subjects[i]);
                subjecttSelected.name = Subjects[i].SubjectName;
                subjecttSelecteds.Add(subjecttSelected);
            }
            return Json(subjecttSelecteds.Select(d => new
            {
                stt = d.stt,
                name = d.name
            }).ToList());
        }

        public IActionResult loadClassSelected(Guid employeeId)
        {
            var classes = DatabaseContext.GetDB.Class.Where(x => x.EmployeeId == employeeId).ToList();

            List<ComboboxSelected> classSelecteds = new List<ComboboxSelected>();
            var ds_Class = DatabaseContext.GetDB.Class.Where(x=>x.EmployeeId==null || x.EmployeeId == employeeId).OrderBy(x => x.ClassName).ToList();
            for (int i = 0; i < classes.Count; i++)
            {
                ComboboxSelected classSelected = new ComboboxSelected();
                classSelected.stt = ds_Class.IndexOf(classes[i]);
                classSelected.name = classes[i].ClassName;
                classSelecteds.Add(classSelected);
            }
            return Json(classSelecteds.Select(d => new
            {
                stt = d.stt,
                name = d.name
            }).ToList());
        }

        public IActionResult EditTeacher([FromBody] TeacherPost data)
        {
            try
            {
                Employee employee = DatabaseContext.GetDB.Employee.Find(Guid.Parse(data.EmployeeId));
                employee.EmployeeName = data.EmployeeName;
                employee.EmployeeCode = data.EmployeeCode;
                employee.Email = data.Email;
                employee.Status = data.Status;
                employee.Address = data.Address;
                employee.PhoneNumber = data.PhoneNumber;
                employee.Gender = int.Parse(data.Gender);
                employee.PlaceOfBirth = data.PlaceOfBirth;
                employee.Nation = data.Nation;
                try
                {
                    employee.DateOfBirth = DateTime.Parse(data.DateOfBirthYear + "-" + data.DateOfBirthMonth + "-" + data.DateOfBirthDate);
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = 501,
                        message = "Ngày sinh",
                    });
                }
                try
                {
                    employee.WorkStarted = DateTime.Parse(data.WorkStartedYear + "-" + data.WorkStartedMonth + "-" + data.WorkStartedDate);
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = 501,
                        message = "Ngày làm việc",
                    });
                }
                try
                {
                    employee.WorkEnded = DateTime.Parse(data.WorkEndedYear + "-" + data.WorkEndedMonth + "-" + data.WorkEndedDate);
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = 501,
                        message = "Ngày kết thúc hợp đồng",
                    });
                }


                DatabaseContext.GetDB.Update(employee);
                if (data.listClass.Count != 0)
                {
                    List<Class> classes = classBAL.GetClassByNameList(data.listClass);                    
                    foreach (Class c in classes)
                    {
                        c.EmployeeId = Guid.Parse(data.EmployeeId);
                    }
                    DatabaseContext.GetDB.SaveChanges();
                }
                // những việc đc gaio lại
                if (data.listSubject.Count != 0)
                {
                    List<Subject> newSubject = subjectBAL.GetSubjectByNameList(data.listSubject);
                    employee.Subject = newSubject;
                }
                if (data.listDepartment.Count != 0)
                {
                    List<Department> departments = departmentBAL.GetDepartmentByNameList(data.listDepartment);
                    
                    int taskUpdate = departmentManagerBAL.AddDepartmentMNGById(Guid.Parse(data.EmployeeId), departments.Select(x => x.DepartmentId).ToList());
                }
                DatabaseContext.GetDB.SaveChanges();
                return Json(new
                {
                    status = 200,
                    message = "Cập nhật thành công!",
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 500,
                    message = ex.Message,
                });
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            TempData["Classes"] = DatabaseContext.GetDB.Class.Where(x => x.EmployeeId == null).OrderBy(x => x.ClassName).ToList();

            TempData["Departments"] = DatabaseContext.GetDB.Department.OrderBy(x => x.DepartmentName).ToList();

            TempData["Subjects"] = DatabaseContext.GetDB.Subject.OrderBy(x => x.SubjectName).ToList();

            return View();
        }

        public string GetTeacherCode()
        {
            string newCode = "";
            using (SqlConnection conn = new SqlConnection(DatabaseContext.ContextString))
            {
                conn.Open();
                using (var command = new SqlCommand("GetNewEmployeeCode", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newCode = reader.GetString(0);
                        }
                    }
                }
            }
            return newCode;
        }
    }
}
