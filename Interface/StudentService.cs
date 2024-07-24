using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MVC_WEB.Models;

public class StudentService : IStudentService
{
    private readonly DBSchoolsContext _context;

    public StudentService(DBSchoolsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoginViewModel>> GetStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<LoginViewModel> GetStudentByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task<LoginViewModel> CreateStudentAsync(LoginViewModel student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<LoginViewModel> UpdateStudentAsync(LoginViewModel student)
    {
        var existingStudent = await _context.Students.FindAsync(student.ID);
        if (existingStudent != null)
        {
            existingStudent.LastName = student.LastName;
            existingStudent.FirstMidName = student.FirstMidName;
            existingStudent.EnrollmentDate = student.EnrollmentDate;
            existingStudent.PhotoPath = student.PhotoPath;
            await _context.SaveChangesAsync();
        }
        return existingStudent;
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
