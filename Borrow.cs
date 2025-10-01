using System.Data.SqlClient;
public class Borrow
{
    private int bookID;
    private int studentID;
    private DateTime borrowDate;
    private DateTime returnDate;

    public Borrow()
    {
        bookID = 0;
        studentID = 0;
        borrowDate = new DateTime();
        returnDate = new DateTime();
    }
    public Borrow(int bookID, int studentID, DateTime borrowDate, DateTime returnDate = new DateTime())
    {
        this.bookID = bookID;
        this.studentID = studentID;
        this.borrowDate = borrowDate;
        this.returnDate = returnDate;
    }

    public int BookID
    {
        get => bookID;
        set => bookID = value;
    }

    public int StudentID
    {
        get => studentID;
        set => studentID = value;
    }

    public DateTime BorrowDate
    {
        get => borrowDate;
        set => borrowDate = value;
    }


    public void BorrowBook(Borrow borrow)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkAvailabilityQuery = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN AND AvailableQuantity > 0";
                using (SqlCommand checkAvailabilityCommand = new SqlCommand(checkAvailabilityQuery, connection))
                {
                    checkAvailabilityCommand.Parameters.AddWithValue("@ISBN", borrow.bookID);
                    int count = (int)checkAvailabilityCommand.ExecuteScalar();
                    if (count <= 0)
                    {
                        Console.WriteLine("Book is not available for borrowing");
                        return;
                    }
                }

                // Generate return date after 15 days
                DateTime returnDate = borrow.BorrowDate.AddDays(15);

                string borrowQuery = "INSERT INTO Borrow (BookID, StudentsID, BorrowingDate, ReturnDate) VALUES (@BookID, @StudentsID, @BorrowingDate, @ReturnDate)";
                string decreaseQuantityQuery = "UPDATE Books SET AvailableQuantity = AvailableQuantity - 1 WHERE ISBN = @ISBN";

                using (SqlCommand borrowCommand = new SqlCommand(borrowQuery, connection))
                using (SqlCommand decreaseQuantityCommand = new SqlCommand(decreaseQuantityQuery, connection))
                {
                    borrowCommand.Parameters.AddWithValue("@BookID", borrow.BookID);
                    borrowCommand.Parameters.AddWithValue("@StudentsID", borrow.StudentID);
                    borrowCommand.Parameters.AddWithValue("@BorrowingDate", borrow.BorrowDate);
                    borrowCommand.Parameters.AddWithValue("@ReturnDate", returnDate);

                    decreaseQuantityCommand.Parameters.AddWithValue("@ISBN", borrow.BookID);

                    // Start a transaction to ensure atomicity of borrowing and quantity update
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        borrowCommand.Transaction = transaction;
                        decreaseQuantityCommand.Transaction = transaction;

                        try
                        {
                            borrowCommand.ExecuteNonQuery();
                            decreaseQuantityCommand.ExecuteNonQuery();
                            transaction.Commit();
                            Console.WriteLine("Book borrowed successfully");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine("Error: " + ex.Message);
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
    
    public void RetrieveBorrowInfo(){
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuerySelect = @"
                    SELECT
	                    Borrow.BookID,
                        Books.BookName,
	                    Borrow.StudentsID,
	                    Borrow.BorrowingDate,
	                    Borrow.ReturnDate
                    FROM
                        LibraryProject.dbo.Books
                    JOIN
                        LibraryProject.dbo.Borrow ON Books.ISBN = Borrow.BookID;";

                SqlCommand command = new SqlCommand(sqlQuerySelect, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("-----------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("Book ID: " + reader["BookID"]);
                        Console.WriteLine("Book Name: " + reader["BookName"]);
                        Console.WriteLine("Students ID: " + reader["StudentsID"]);
                        Console.WriteLine("Borrowing Date: " + reader["BorrowingDate"]);
                        Console.WriteLine("Return Date: " + reader["ReturnDate"]);
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
}