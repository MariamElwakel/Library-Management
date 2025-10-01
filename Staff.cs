using System.Data.SqlClient;
using System;
public class Staff : User
{
    private int SSN;
    private int officeHours;
    private int salary;

    public Staff(){
        SSN = 0;
        officeHours = 0;
        salary = 0;
    }

    public Staff(int hours, int s, string name, string email, string password, string address, string gender,int phone, DateTime birthdate, int age=0)
        : base(name, email ,password, address, gender, phone, birthdate,age)
    {
        this.officeHours = hours;
        this.salary = s;
    }

    public int getOfficeHours(){
        return officeHours;
    }

    public int getSalary(){
        return salary;
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
    public int GetPhone()
    {
        return base.getPhone();
    }

    public string GetPassword()
    {
        return base.getPassword();
    }

    public void AddAdminToDatabase(Staff admin)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DateTime birthDate = admin.GetBirthDate();
                DateTime today = DateTime.Now;
                int age = today.Year - birthDate.Year;

                // Check if the birthday for the current year has passed
                if (birthDate > today.AddYears(-age)){
                    age--;
                }

                string query = "INSERT INTO Users (Name, Email,Password, Address, Gender, BirthDate, Age,Phone) " +
                           "VALUES (@Name, @Email,@Password, @Address, @Gender, @BirthDate, @Age,@Phone); " +
                           "DECLARE @UserID INT = SCOPE_IDENTITY(); " +
                           "INSERT INTO Staff (OfficeHours, Salary, UserID) " +
                           "VALUES (@OfficeHours, @Salary, @UserID)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", admin.GetName());
                command.Parameters.AddWithValue("@Email", admin.GetEmail());
                command.Parameters.AddWithValue("@Password", admin.GetPassword());
                command.Parameters.AddWithValue("@Address", admin.GetAddress());
                command.Parameters.AddWithValue("@Gender", admin.GetGender());
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Phone", admin.GetPhone());
                command.Parameters.AddWithValue("@OfficeHours", admin.getOfficeHours());
                command.Parameters.AddWithValue("@Salary", admin.getSalary());

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Admind added successfully!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }


    public void UpdateAdminDetails(int adminID)
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

                string userQuery = "SELECT UserID FROM Staff WHERE SSN = @SSN;";
                int userID;
                using (SqlCommand userCommand = new SqlCommand(userQuery, connection))
                {
                    userCommand.Parameters.AddWithValue("@SSN", adminID);
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
                        Console.WriteLine(rowsAffected + " row is updated");
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
                        Console.WriteLine(rowsAffected + " row is updated");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                else
                {
                    Console.WriteLine("Admin SSN not found");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void DeleteAdmin(int adminssn)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string studentQuery = "SELECT UserID FROM Staff WHERE SSN = @SSN;";
                int userID;
                using (SqlCommand studentCommand = new SqlCommand(studentQuery, connection))
                {
                    studentCommand.Parameters.AddWithValue("@SSN", adminssn);
                    userID = Convert.ToInt32(studentCommand.ExecuteScalar());
                }

                if (userID != 0)
                {
                    // Remove the foreign key constraint in the Students table
                    string deleteStudentsQuery = "DELETE FROM Staff WHERE SSN = @SSN;";
                    using (SqlCommand deleteStudentsCommand = new SqlCommand(deleteStudentsQuery, connection))
                    {
                        deleteStudentsCommand.Parameters.AddWithValue("@SSN", adminssn);
                        deleteStudentsCommand.ExecuteNonQuery();
                    }

                    // Delete the user from the Users table
                    string deleteUserQuery = "DELETE FROM Users WHERE UserID = @UserID;";
                    using (SqlCommand deleteUserCommand = new SqlCommand(deleteUserQuery, connection))
                    {
                        deleteUserCommand.Parameters.AddWithValue("@UserID", userID);
                        int rowsAffected = deleteUserCommand.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row is deleted");
                    }
                }
                else
                {
                    Console.WriteLine("Admin SSN not found");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void RetrieveAdminDetails()
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuerySelect = @"
                    SELECT
                        Staff.UserID,
                        Staff.SSN,
                        Users.Name,
                        Users.Email,
                        Users.Password,
                        Users.Gender,
                        Users.Address,
                        Staff.OfficeHours,
                        Staff.Salary,
                        Users.BirthDate,
                        Users.Age,
                        Users.Phone
                    FROM
                        LibraryProject.dbo.Staff
                    JOIN
                        LibraryProject.dbo.Users ON Staff.UserID = Users.UserID;";

                SqlCommand command = new SqlCommand(sqlQuerySelect, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("-----------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("User ID: " + reader["UserID"]);
                        Console.WriteLine("Admin SSN: " + reader["SSN"]);
                        Console.WriteLine("Name: " + reader["Name"]);
                        Console.WriteLine("Email: " + reader["Email"]);
                        Console.WriteLine("Password: " + reader["Password"]);
                        Console.WriteLine("Gender: " + reader["Gender"]);
                        Console.WriteLine("Address: " + reader["Address"]);
                        Console.WriteLine("Office Hours: " + reader["OfficeHours"]);
                        Console.WriteLine("Salary: " + reader["Salary"]);
                        Console.WriteLine("BirthDate: " + reader["BirthDate"]);
                        Console.WriteLine("Age: " + reader["Age"]);
                        Console.WriteLine("Phone: " + reader["Phone"]);
                        Console.WriteLine("-----------------------------");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public void RetrieveStudentDetails()
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuerySelect = @"
                    SELECT
                        Students.UserID,
                        Students.StudentID,
                        Users.Name,
                        Users.Email,
                        Users.Password,
                        Users.Gender,
                        Users.Address,
                        Students.department,
                        Users.BirthDate,
                        Users.Age,
                        Users.Phone
                    FROM
                        LibraryProject.dbo.Students
                    JOIN
                        LibraryProject.dbo.Users ON Students.UserID = Users.UserID;";

                SqlCommand command = new SqlCommand(sqlQuerySelect, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("-----------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("User ID: " + reader["UserID"]);
                        Console.WriteLine("Student ID: " + reader["StudentID"]);
                        Console.WriteLine("Name: " + reader["Name"]);
                        Console.WriteLine("Email: " + reader["Email"]);
                        Console.WriteLine("Password: " + reader["Password"]);
                        Console.WriteLine("Gender: " + reader["Gender"]);
                        Console.WriteLine("Address: " + reader["Address"]);
                        Console.WriteLine("department: " + reader["department"]);
                        Console.WriteLine("BirthDate: " + reader["BirthDate"]);
                        Console.WriteLine("Age: " + reader["Age"]);
                        Console.WriteLine("Phone: " + reader["Phone"]);
                        Console.WriteLine("-----------------------------");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }


    public void GiveFineToStudent(Fines fine)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Fines (StudentsID, FineAmount, FineDate, FineReason) VALUES (@StudentsID, @FineAmount, @FineDate, @FineReason)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@StudentsID", fine.getStudentID());
                command.Parameters.AddWithValue("@FineAmount", fine.getFineAmount());
                command.Parameters.AddWithValue("@FineDate", fine.getFineDate());
                command.Parameters.AddWithValue("@FineReason", fine.getFineReason());

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Fine added successfully");
                }
                else
                {
                    Console.WriteLine("Failed to add fine");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

   


    public Book findBook(int isbn)
    {
        Book book = null;
        int numOfBooks = 0;
        string sectionName = "";
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books WHERE ISBN = @ISBN;";
                SqlCommand command = new SqlCommand(sqlQuerySelect, connection);
                command.Parameters.AddWithValue("@ISBN", isbn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int sectionID = Convert.ToInt32(reader["SectionID"]);
                        string bookName = reader["BookName"].ToString();
                        string author = reader["Author"].ToString();
                        string publisher = reader["Publisher"].ToString();
                        int noCopies = Convert.ToInt32(reader["NoCopies"]);
                        int availableQuantity = Convert.ToInt32(reader["AvailableQuantity"]);
                        int publishingYear = Convert.ToInt32(reader["PublishingYear"]);

                        reader.Close(); // Close the first SqlDataReader before executing the sectionCommand

                        string sectionQuery = "SELECT SectionName, NumBooks FROM LibraryProject.dbo.Sections WHERE SectionID = @SectionID;";
                        using (SqlCommand sectionCommand = new SqlCommand(sectionQuery, connection))
                        {
                            sectionCommand.Parameters.AddWithValue("@SectionID", sectionID);

                            using (SqlDataReader sectionReader = sectionCommand.ExecuteReader())
                            {
                                if (sectionReader.Read())
                                {
                                    sectionName = sectionReader["SectionName"].ToString();
                                    numOfBooks = Convert.ToInt32(sectionReader["NumBooks"]);
                                }
                            }
                        }   

                        // Obtain the SectionID value or provide it directly
                        int sectionIDValue = sectionID;

                        Section s = new Section(numOfBooks, sectionName);
                        book = new Book(bookName, author, publisher, noCopies, availableQuantity, publishingYear, sectionIDValue, isbn);

                        // Print the book information
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Book Found:");
                        Console.WriteLine("ISBN: " + isbn);
                        Console.WriteLine("Book Name: " + bookName);
                        Console.WriteLine("Author: " + author);
                        Console.WriteLine("Publisher: " + publisher);
                        Console.WriteLine("No. of Copies: " + noCopies);
                        Console.WriteLine("Available Quantity: " + availableQuantity);
                        Console.WriteLine("Publishing Year: " + publishingYear);
                        Console.WriteLine("Section Name: " + sectionName);
                        Console.WriteLine("Number of Books in Section: " + numOfBooks);
                        Console.WriteLine("-----------------------------");

                        return book;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        return null;
    }

    public void AddBookToDatabase(Book book)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string bookQuery = "INSERT INTO Books (BookName, Author, Publisher, PublishingYear, NoCopies, AvailableQuantity, SectionID) " +
                               "VALUES (@BookName, @Author, @Publisher, @PublishingYear, @NoCopies, @AvailableQuantity, @SectionID);";

                using (SqlCommand command = new SqlCommand(bookQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookName", book.getBookName());
                    command.Parameters.AddWithValue("@Author", book.getAuthor());
                    command.Parameters.AddWithValue("@Publisher", book.getPublisher());
                    command.Parameters.AddWithValue("@PublishingYear", book.getPublishingYear());
                    command.Parameters.AddWithValue("@NoCopies", book.getNoCopies());
                    command.Parameters.AddWithValue("@AvailableQuantity", book.getAvailableQuantity());
                    command.Parameters.AddWithValue("@SectionID", book.GetSection());

                    command.ExecuteNonQuery();
                    Console.WriteLine("Book added successfully");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    public void UpdateBookData(int isbn)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        Console.WriteLine("What do you want to update?\n1-Available Quantity\n2-No of Copies\n3-Delete the book");
        int choice = Convert.ToInt32(Console.ReadLine());
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string userQuery = "SELECT ISBN FROM LibraryProject.dbo.Books WHERE ISBN = @ISBN;";

                using (SqlCommand userCommand = new SqlCommand(userQuery, connection))
                {
                    userCommand.Parameters.AddWithValue("@ISBN", isbn);
                    isbn = Convert.ToInt32(userCommand.ExecuteScalar());
                }
                if (isbn != 0)
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("Enter the new Quantity:");
                        int newavailableQuantity = Convert.ToInt32(Console.ReadLine());
                        string sqlQueryUpdate = "UPDATE LibraryProject.dbo.Books SET availableQuantity = @availableQuantity WHERE ISBN = @ISBN;";
                        SqlCommand command = new SqlCommand(sqlQueryUpdate, connection);
                        command.Parameters.AddWithValue("@availableQuantity", newavailableQuantity);
                        command.Parameters.AddWithValue("@ISBN", isbn);
                        command.ExecuteNonQuery();
                        Console.Write("Available Quantity updated successfully");
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Enter the new number of copies:");
                        int NewNoCopies = Convert.ToInt32(Console.ReadLine());
                        
                        string sqlQueryUpdate = "UPDATE LibraryProject.dbo.Books SET NoCopies = @newNoCopies WHERE ISBN = @ISBN;";
                        SqlCommand command = new SqlCommand(sqlQueryUpdate, connection);
                        command.Parameters.AddWithValue("@newNoCopies", NewNoCopies);
                        command.Parameters.AddWithValue("@ISBN", isbn);
                        command.ExecuteNonQuery();
                        Console.Write("Numbers of copies updated successfully");
                    }
                    else if (choice == 3)
                    {
                        string sqlQueryUpdate = "delete from LibraryProject.dbo.Books WHERE ISBN = @ISBN;";
                        SqlCommand command = new SqlCommand(sqlQueryUpdate, connection);
                        command.Parameters.AddWithValue("@ISBN", isbn);
                        command.ExecuteNonQuery();
                        Console.Write("Book deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                else
                {
                    Console.WriteLine("Book ID not found");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
    
}