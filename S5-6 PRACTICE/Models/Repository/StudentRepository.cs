using Example.Data;
using Example.Models.Domain;
using Example.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using S5_6_PRACTICE.Data;
using S5_6_PRACTICE.Models.Domain;
using S5_6_PRACTICE.Models.Repository;
using S5_6_PRACTICE.Models.ViewModels;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Example.Models.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolDbContext dbContext;
        public StudentRepository(SchoolDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Student> GetAll()
        {
            return dbContext.Students;
        }

        public VMStudent? GetStudentsById(int id)
        {
            var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (student != null)
            {
                string GenderVm;
                if (student.Gender == false) GenderVm = "female"; else GenderVm = "male";
                var studentVM = new VMStudent()
                {
                    Id = id,
                    Name = student.Name,
                    Birth = student.Birth,
                    ImgUrl = student.ImgUrl,
                    Gender = GenderVm,
                    Mssv = student.Mssv,
                    Description = student.Description,
                };
                return studentVM;
            }
            return null;
        }


        public void UpdateStudentById(int id, VMStudent model)
        {
            var StudentById = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (StudentById != null)
            {
                StudentById.Name = model.Name;
                StudentById.Birth = model.Birth;
                if (model.Gender == "male") StudentById.Gender = true; else StudentById.Gender = false;
                StudentById.ImgUrl = model.ImgUrl;
                StudentById.Mssv = model.Mssv;
                StudentById.Description = model.Description;
                dbContext.Update(StudentById);
                dbContext.SaveChanges();
            }
        }

        public void AddStudent(VMStudent Model)
        {
            bool GenderData;
            if (Model.Gender == "male")
                GenderData = true;
            else GenderData = false;

            var student = new Student()
            {
                Name = Model.Name,
                Birth = Model.Birth,
                Gender = GenderData,
                ImgUrl = Model.ImgUrl,
                Mssv = Model.Mssv,
                Description = Model.Description
            };

            dbContext.Students.Add(student);
            dbContext.SaveChanges();
        }

        public void DeleteStudentById(int id)
        {
            var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
        }
    }
}