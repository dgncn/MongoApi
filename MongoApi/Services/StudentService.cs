using MongoApi.Models;
using MongoApi.Models.SchoolDbSettings;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoApi.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
        }

        public List<Student> Get() =>
            _students.Find(student => true).ToList();

        public Student Get(string id) =>
            _students.Find<Student>(student => student.Id == id).FirstOrDefault();

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student newStudent) =>
            _students.ReplaceOne(student => student.Id == id, newStudent);

        public void Remove(Student _student) =>
            _students.DeleteOne(student => student.Id == _student.Id);

        public void Remove(string id) =>
            _students.DeleteOne(student => student.Id == id);
    }
}
