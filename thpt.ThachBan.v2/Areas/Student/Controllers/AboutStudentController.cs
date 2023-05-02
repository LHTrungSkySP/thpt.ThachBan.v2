using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using thpt.ThachBan.BAL.StudentBAL;
using thpt.ThachBan.DAL;
using thpt.ThachBan.DTO.Models;

namespace thpt.ThachBan.v2.Controllers
{
    public class AboutStudentController : Controller
    {
        #region Feild
        private IStudentBAL studentBAL;
        #endregion

        #region Contructor
        public AboutStudentController(IStudentBAL studentBAL)
        {
            this.studentBAL = studentBAL;
        }
        #endregion
        public IActionResult Index()
        {
            dynamic data = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserInfor"));
            string code= data.AccountCode;


            return View(studentBAL.GetAboutStudent(code));
        }
    }
}
