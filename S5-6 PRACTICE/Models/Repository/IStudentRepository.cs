using Example.Models.Domain;
using Example.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace S5_6_PRACTICE.Models.Repository
{
    public interface IStudentRepository
    {
        public IEnumerable<Student> GetAll(string? searchString,string? Type);
        public VMStudent GetStudentById(int id);
        public void UpdateStudentById(int id, VMStudent models);
        public void AddStudent(VMStudent models);
        public void DeleteStudent(int id);
    }
}
