using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace SqLite_Windows10
{
    public sealed partial class MainPage : Page
    {
        Database db = new Database();

        public MainPage()
        {
            this.InitializeComponent();

            db.CreateDatabase();

            PrintStudentList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int studentNumber = Convert.ToInt32(number.Text.Trim());
            string studentName = name.Text.Trim();
            string studentDepartment = department.Text.Trim();

            Database.Student newStudent = new Database.Student(studentName, studentNumber, studentDepartment);
            db.Insert(newStudent);

            PrintStudentList();
        }

        private void PrintStudentList()
        {
            ObservableCollection<Database.Student> listofStudents = new ObservableCollection<Database.Student>();
            listofStudents = db.ReadAllStudent();
            studentListBox.ItemsSource = listofStudents.ToList();
        }

    }
}
