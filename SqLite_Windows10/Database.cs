
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Platform.WinRT;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace SqLite_Windows10
{
    public class Database
    {

        public void CreateDatabase()
        {
            var sqlPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "StudentDB.sqlite");

            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath))
            {
                conn.CreateTable<Student>();
            }
        }

        public void Insert(Student student)
        {
            var sqlPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "StudentDB.sqlite");

            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(student);
                });
            }
        }

        public void Update(Student student)
        {
            var sqlPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "StudentDB.sqlite");

            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath))
            {
                var existingStudent = conn.Query<Student>("select * from Students where Number =" + student.Number).FirstOrDefault();

                if (existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Number = student.Number;
                    existingStudent.Department = student.Department;

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(existingStudent);
                    });
                }
            }
        }

        public void Delete(int Number)
        {
            var sqlPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "StudentDB.sqlite");

            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath))
            {
                var existingconact = conn.Query<Student>("select * from Students where Number =" + Number).FirstOrDefault();
                if (existingconact != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingconact);
                    });
                }
            }
        }
        
        public void DeleteAll()
        {
            var sqlPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "StudentDB.sqlite");

            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath))
            {
                conn.DropTable<Student>();
                conn.CreateTable<Student>();
                conn.Dispose();
                conn.Close();
            }
        }

        public ObservableCollection<Student> ReadAllStudent()
        {
            var sqlPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "StudentDB.sqlite");

            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath))
            {
                List<Student> collection = conn.Table<Student>().ToList<Student>();
                ObservableCollection<Student> studentList = new ObservableCollection<Student>(collection);
                return studentList;
            }

        }


        public class Student
        {
            [PrimaryKey, AutoIncrement]

            public int Id { get; set; }

            public string Name { get; set; }

            public int Number { get; set; }

            public string Department { get; set; }


            public Student()
            {

            }

            public Student(string Name, int Number, string Department)
            {
                this.Name = Name;
                this.Number = Number;
                this.Department = Department;
            }

        }

    }
}
