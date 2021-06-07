using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Console;


namespace SimpleUniversityApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static readonly StudentsService StudentsService = new StudentsService();

        private static bool MainMenu()
        {
            var students = StudentsService.GetAllStudents();
            Clear();
            ShowStudentsData(students);
            WriteLine("Choose an option:");
            WriteLine("1. Create");
            WriteLine("2. Update");
            WriteLine("3. Delete");
            WriteLine("4. Exit");
            Write("\r\nSelect an option: ");

            switch (ReadLine())
            {
                case "1":
                    var createdStudent = SetStudentData();
                    var isValidCreatedStudent = ValidateStudentProperty(createdStudent);
                    if (isValidCreatedStudent)
                    {
                        StudentsService.CreateStudent(createdStudent);
                        WriteLine($"Student {createdStudent.FirstName} {createdStudent.LastName} successful added");
                    }
                    else
                    {
                        WriteLine("not valid data");
                    }
                    
                    Write("\r\nPress Enter to return to Main Menu");
                    ReadLine();
                    return true;
                case "2":
                    WriteLine("Enter ID of student to update");
                    var updateId = int.Parse(ReadLine() ?? string.Empty);
                    var updatedStudent = SetStudentData();
                    var isValidUpdatedStudent = ValidateStudentProperty(updatedStudent);
                    if (isValidUpdatedStudent)
                    {
                        StudentsService.UpdateStudent(updatedStudent, updateId);
                        WriteLine($"Student {updatedStudent.FirstName} {updatedStudent.LastName} successful updated");
                    }
                    else
                    {
                        WriteLine("not valid data");
                    }

                    Write("\r\nPress Enter to return to Main Menu");
                    ReadLine();
                    return true;
                case "3":
                    WriteLine("Enter ID of student to delete");
                    var deleteId = int.Parse(ReadLine() ?? throw new InvalidOperationException());
                    StudentsService.DeleteStudent(deleteId);
                    Write("\r\nPress Enter to return to Main Menu");
                    ReadLine();
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }

        private static StudentModel SetStudentData()
        {
            var student = new StudentModel();
            Clear();
            WriteLine("enter first name");
            student.FirstName = ReadLine();
            WriteLine("enter last name");
            student.LastName = ReadLine();
            WriteLine("enter phone number");
            student.PhoneNumber = ReadLine();
            WriteLine("enter birth date (yyyy-mm-dd)");
            student.BirthDate = DateTime.Parse(ReadLine());
            WriteLine("enter email");
            student.Email = ReadLine();
            WriteLine("enter address");
            student.Address = ReadLine();
            
            return student;
        }

        private static void ShowStudentsData(List<StudentModel> students)
        {
            WriteLine("Student ID \tFirst Name \tLast Name \tPhone Number \tBirth Date \tEmail \tAddress");
            foreach (var student in students)
            {
                WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6}", student.StudentId, student.FirstName,
                                        student.LastName, student.PhoneNumber, student.BirthDate.ToShortDateString(), student.Email, student.Address);
            }
        }

        private static bool ValidateStudentProperty(StudentModel student)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(student);

            if (!Validator.TryValidateObject(student, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return false;
            }

            return true;
        }
    }
}
