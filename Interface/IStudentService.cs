using System.Collections.Generic;
using System.Threading.Tasks;
using MVC_WEB.Models;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(Student student);
    Task<Student> UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}
