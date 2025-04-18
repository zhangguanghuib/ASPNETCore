using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmentsRepository departmentsRepository;
        private readonly IEmployeesRepository employeesRepository;

        public CreateModel(
            IDepartmentsRepository departmentsRepository, 
            IEmployeesRepository employeesRepository)
        {
            this.departmentsRepository = departmentsRepository;
            this.employeesRepository = employeesRepository;
        }

        [BindProperty]
        public EmployeeViewModel? EmployeeViewModel { get; set; }

        public void OnGet()
        {
            this.EmployeeViewModel = new EmployeeViewModel();
            this.EmployeeViewModel.Employee = new Employee();
            this.EmployeeViewModel.Departments = departmentsRepository.GetDepartments();
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelStateHelper.GetErrors(this.ModelState);   
                return RedirectToPage("/Error", new { errors});
            }

            if (this.EmployeeViewModel is not null && this.EmployeeViewModel.Employee is not null)
            {
                employeesRepository.AddEmployee(this.EmployeeViewModel.Employee);
            }

            return RedirectToPage("Index");
        }
    }
}
