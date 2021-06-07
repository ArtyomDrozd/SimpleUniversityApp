using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SimpleUniversityApp
{
    class StudentsService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<StudentModel> GetAllStudents()
        {
            var students = new List<StudentModel>();
            const string sqlExpression = "SELECT * FROM students";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new MySqlCommand(sqlExpression, connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var student = new StudentModel
                            {
                                StudentId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                PhoneNumber = reader.GetString(3),
                                BirthDate = reader.GetDateTime(4),
                                Email = reader.GetString(5),
                                Address = reader.GetString(6)
                            };
                            students.Add(student);
                        }
                        connection.Close();
                    }
                }
                catch
                {
                    Console.WriteLine("SQL ERROR");
                }
            }

            return students;
        }

        public void CreateStudent(StudentModel createdStudent)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new MySqlCommand(
                        "INSERT INTO students(first_name, last_name, phone_number, birth_date, email, address) " +
                        "VALUES(@first_name, @last_name, @phone_number, @birth_date, @email, @address)", connection);
                    command.Parameters.AddWithValue("@first_name", createdStudent.FirstName);
                    command.Parameters.AddWithValue("@last_name", createdStudent.LastName);
                    command.Parameters.AddWithValue("@phone_number", createdStudent.PhoneNumber);
                    command.Parameters.AddWithValue("@birth_date", createdStudent.BirthDate);
                    command.Parameters.AddWithValue("@email", createdStudent.Email);
                    command.Parameters.AddWithValue("@address", createdStudent.Address);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (MySqlException e)
                {

                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
        }

        public void UpdateStudent(StudentModel updatedStudent, int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new MySqlCommand(
                        "update students set first_name = @first_name, last_name = @last_name, phone_number = @phone_number," +
                        " birth_date = @birth_date, email = @email, address =@adress where studentid = @studentid",
                        connection);
                    command.Parameters.AddWithValue("@studentid", id);
                    command.Parameters.AddWithValue("@first_name", updatedStudent.FirstName);
                    command.Parameters.AddWithValue("@last_name", updatedStudent.LastName);
                    command.Parameters.AddWithValue("@phone_number", updatedStudent.PhoneNumber);
                    command.Parameters.AddWithValue("@birth_date", updatedStudent.BirthDate);
                    command.Parameters.AddWithValue("@email", updatedStudent.Email);
                    command.Parameters.AddWithValue("@adress", updatedStudent.Address);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command =
                        new MySqlCommand("delete from students where studentid = @studentid", connection);
                    command.Parameters.AddWithValue("@studentid", id);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR:" + e.Message);
                }
            }
        }
    }
}
