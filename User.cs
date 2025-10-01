using System.Data.SqlClient;
public class User
{
    private int UserID;
    private string name;
    private string email;
    private string password;
    private string address;
    private string gender;
    private DateTime birthdate;
    private int age;
    private int phone;

    public User(){
        UserID = 0;
        name = "";
        email = "";
        password = "";
        address = "";
        gender = "";
        phone = 0;
        birthdate = new DateTime();
        age = 0;
    }
    public User(string name, string email, string password, string address, string gender,int phone, DateTime birthdate, int age)
    {
        this.name = name;
        this.email = email;
        this.password = password;
        this.address = address;
        this.gender = gender;
        this.phone = phone;
        this.birthdate = birthdate;
        this.age = age;
    }


    public string getName(){
        return name;
    }

    public string getEmail(){
        return email;
    }

    public string getAddress(){
        return address;
    }

    public string getGender(){
        return gender;
    }

    public DateTime getBirthDate(){
        return birthdate;
    }

    public string getPassword()
    {
        return password;
    }
    public int getPhone()
    {
        return phone;
    }
    public void browseBooks()
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books";
                SqlCommand command = new SqlCommand(sqlQuerySelect, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("-----------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("ISBN: " + reader["ISBN"]);
                        Console.WriteLine("Book Name: " + reader["BookName"]);
                        Console.WriteLine("Publisher: " + reader["Publisher"]);
                        Console.WriteLine("Author: " + reader["Author"]);
                        Console.WriteLine("Section ID: " + reader["SectionID"]);
                        Console.WriteLine("Publication Year: " + reader["PublishingYear"]);
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

    public void searchBook()
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        string sqlQuerySelect;
        Console.WriteLine("Choose an option:\n1-ISBN\n2-Book Name\n3-Publisher\n4-Author\n5-Publication Year:");
        int choice = Convert.ToInt32(Console.ReadLine());

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (choice == 1)
                {
                    Console.WriteLine("Enter the book's ISBN:");
                    int isbn = Convert.ToInt32(Console.ReadLine());
                    sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books WHERE ISBN = " + isbn;
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter the book's name:");
                    string bookname = Console.ReadLine();
                    sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books WHERE BookName = '" + bookname + "';";
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter the Publisher:");
                    string Publisher = Console.ReadLine();
                    sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books WHERE Publisher = '" + Publisher + "';";
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Enter the Author's name:");
                    string Author = Console.ReadLine();
                    sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books WHERE Author = '" + Author + "';";
                }
                else if (choice == 5)
                {
                    Console.WriteLine("Enter the Publication Year:");
                    int publishingYear = Convert.ToInt32(Console.ReadLine());
                    sqlQuerySelect = "SELECT * FROM LibraryProject.dbo.Books WHERE PublishingYear = " + publishingYear;
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                    return;
                }

                using (SqlCommand command = new SqlCommand(sqlQuerySelect, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("-----------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine("ISBN: " + reader["ISBN"]);
                            Console.WriteLine("Book Name: " + reader["BookName"]);
                            Console.WriteLine("Publisher: " + reader["Publisher"]);
                            Console.WriteLine("Author: " + reader["Author"]);
                            Console.WriteLine("Section ID: " + reader["SectionID"]);
                            Console.WriteLine("Publication Year: " + reader["PublishingYear"]);
                            Console.WriteLine("-----------------------------");
                        }
                    }    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public bool Login()
    {
        bool isAuthenticated = false;
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Enter your email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine();

                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();
                    isAuthenticated = count > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        return isAuthenticated;
    }
}
