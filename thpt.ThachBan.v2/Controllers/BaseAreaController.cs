using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using thpt.ThachBan.DAL;

namespace thpt.ThachBan.v2.Controllers
{
    public class BaseAreaController : Controller
    {
        public IActionResult Index()
        {
            dynamic data = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserInfor"));
            string code = data.AccountCode.ToString();
            int role = data.Role.RoleGroup;
            if (role == 0 || role ==1)
            {
                TempData["Name"] = DatabaseContext.GetDB.Employee.Where(x => x.EmployeeCode == code).FirstOrDefault().EmployeeName;
            }
            else if (role== 2)
            {
                TempData["Name"] = DatabaseContext.GetDB.Student.Where(x => x.StudentCode == code).FirstOrDefault().StudentName;
            }
            TempData["GroupRole"] = role;

            TempData.Keep("Name");
            TempData.Keep("GroupRole");
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserInfor");
            return Redirect("/Login");
        }
        

    }
}
