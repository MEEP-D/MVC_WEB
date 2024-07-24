using System.Collections.Generic;
using System.Threading.Tasks;
using MVC_WEB.Models;

public interface IStudentService
{
    Task<IEnumerable<LoginViewModel>> GetStudentsAsync();
    Task<LoginViewModel> GetStudentByIdAsync(int id);
    Task<LoginViewModel> CreateStudentAsync(LoginViewModel student);
    Task<LoginViewModel> UpdateStudentAsync(LoginViewModel student);
    Task DeleteStudentAsync(int id);
}
