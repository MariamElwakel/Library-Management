using System.Data.SqlClient;
using System;
public class Student : User
{
    private int studentID;
    private string department;

    public Student(){
        studentID = 0;
        department = "";
    }
    public Student(string dep, string name, string email , string password, string address, string gender, int phone, DateTime birthdate, int age = 0)
        : base(name, email,password, address, gender, phone, birthdate,age)
    {
        this.department = dep;
    }

    public string getStudentDep(){
        return department;
    }

    public string GetName()
    {
        return base.getName();
    }

    public string GetEmail()
    {
        return base.getEmail();
    }

    public string GetAddress()
    {
        return base.getAddress();
    }

    public string GetGender()
    {
        return base.getGender();
    }

    public DateTime GetBirthDate()
    {
        return base.getBirthDate();
    }
    public string GetPassword()
    {
        return base.getPassword();
    }

    public int GetPhone()
    {
        return base.getPhone();
    }
    

    public void AddStuentToDatabase(Student student)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DateTime birthDate = student.GetBirthDate();
                DateTime today = DateTime.Now;
                int age = today.Year - birthDate.Year;

                // Check if the birthday for the current year has passed
                if (birthDate > today.AddYears(-age)){
                    age--;
                }

                string query = "INSERT INTO Users (Name, Email, Password, Gender, Address, BirthDate, Age,Phone) " +
                           "VALUES (@Name, @Email, @Password, @Gender, @Address, @BirthDate, @Age, @Phone); " +
                           "DECLARE @UserID INT = SCOPE_IDENTITY(); " +
                           "INSERT INTO Students (Department, UserID) " +
                           "VALUES (@Department, @UserID)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", student.GetName());
                command.Parameters.AddWithValue("@Email", student.GetEmail());
                command.Parameters.AddWithValue("@Password", student.GetPassword());
                command.Parameters.AddWithValue("@Address", student.GetAddress());
                command.Parameters.AddWithValue("@Gender", student.GetGender());
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Phone", student.GetPhone());
                command.Parameters.AddWithValue("@Department", student.getStudentDep());

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Student added successfully!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void UpdateStudentDetails(int StudentID)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        Console.WriteLine("What do you want to update?");
        Console.WriteLine("1-Email");
        Console.WriteLine("2-Password");

        int choice = Convert.ToInt32(Console.ReadLine());

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string userQuery = "SELECT UserID FROM Students WHERE StudentID = @StudentID;";
                int userID;
                using (SqlCommand userCommand = new SqlCommand(userQuery, connection))
                {
                    userCommand.Parameters.AddWithValue("@StudentID", StudentID);
                    userID = Convert.ToInt32(userCommand.ExecuteScalar());
                }

                if (userID != 0)
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("Enter the new email:");
                        string newEmail = Console.ReadLine();

                        string sqlQueryUpdate = "UPDATE LibraryProject.dbo.Users SET Email = @newEmail WHERE UserID = @UserID;";
                        SqlCommand command = new SqlCommand(sqlQueryUpdate, connection);
                        command.Parameters.AddWithValue("@newEmail", newEmail);
                        command.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row updated");
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Enter the new Password:");
                        string newPassword = Console.ReadLine();

                        string sqlQueryUpdate = "UPDATE LibraryProject.dbo.Users SET Password = @newPassword WHERE UserID = @UserID;";
                        SqlCommand command = new SqlCommand(sqlQueryUpdate, connection);
                        command.Parameters.AddWithValue("@newPassword", newPassword);
                        command.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row updated");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                else
                {
                    Console.WriteLine("Student ID not found");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void DeleteStudent(int studentID)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string studentQuery = "SELECT UserID FROM Students WHERE StudentID = @StudentID;";
                int userID;
                using (SqlCommand studentCommand = new SqlCommand(studentQuery, connection))
                {
                    studentCommand.Parameters.AddWithValue("@StudentID", studentID);
                    userID = Convert.ToInt32(studentCommand.ExecuteScalar());
                }

                if (userID != 0)
                {
                    // Remove the foreign key constraint in the Students table
                    string deleteStudentsQuery = "DELETE FROM Students WHERE StudentID = @StudentID;";
                    using (SqlCommand deleteStudentsCommand = new SqlCommand(deleteStudentsQuery, connection))
                    {
                        deleteStudentsCommand.Parameters.AddWithValue("@StudentID", studentID);
                        deleteStudentsCommand.ExecuteNonQuery();
                    }

                    // Delete the user from the Users table
                    string deleteUserQuery = "DELETE FROM Users WHERE UserID = @UserID;";
                    using (SqlCommand deleteUserCommand = new SqlCommand(deleteUserQuery, connection))
                    {
                        deleteUserCommand.Parameters.AddWithValue("@UserID", userID);
                        int rowsAffected = deleteUserCommand.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row deleted");
                    }
                }
                else
                {
                    Console.WriteLine("Student ID not found");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

}