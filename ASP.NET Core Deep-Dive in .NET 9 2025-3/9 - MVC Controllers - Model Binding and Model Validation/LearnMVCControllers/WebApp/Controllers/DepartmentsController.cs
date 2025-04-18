using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    
    public class DepartmentsController
    {
            
        public string Index()
        {
            return "These are the departments.";
        }
          
        public string Details(int? id)
        {
            return $"Department info: {id}";
        }

        [HttpPost]
        public object Create(Department department)
        {
            return department;
        }

        [HttpPost]
        public string Delete(int? id)
        {
            return $"Deleted department: {id}";
        }

        [HttpPost]
        public string Edit(int? id)
        {
            return $"Updated department: {id}";
        }
      
    }
}
