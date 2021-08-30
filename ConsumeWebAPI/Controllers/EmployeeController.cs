using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumeWebAPI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient client = null;
        private string employeesApiUrl = "";
        public EmployeeController(HttpClient client, IConfiguration config)
        {
            this.client = client;
            employeesApiUrl = config.GetValue<string>("AppSettings:EmployeesApiUrl");
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringdata = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Employee> data = JsonSerializer.Deserialize<List<Employee>>(stringdata, options);

            return View(data);
        }
    }
}
