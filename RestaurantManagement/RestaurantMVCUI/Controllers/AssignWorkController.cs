using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace RestaurantMVCUI.Controllers
{
    public class AssignWorkController : Controller
    {
        IConfiguration _configuration;
        public AssignWorkController(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public async Task<IActionResult> Index(int EmpId)
        {
            int data = Convert.ToInt32(TempData["OrderIdforAssign"]);
            TempData.Keep();
            AssignWork assignWork = new AssignWork();
            assignWork.EmpId = EmpId;
            assignWork.OrderId = data;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(assignWork), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/AddAssignWork";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Work Assigned Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Not Assigned";
                    }

                }
            }
            return View();
        }



    }
}
