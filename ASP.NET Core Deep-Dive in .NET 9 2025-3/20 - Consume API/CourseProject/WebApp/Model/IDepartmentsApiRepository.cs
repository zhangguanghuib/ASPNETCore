
namespace WebApp.Models
{
    public interface IDepartmentsApiRepository
    {
        Task<Department?> GetDepartmentByIdAsync(int id);
    }
}