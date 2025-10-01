using System.Data.SqlClient;
public class Fines
{
    private int StudentID;
    private double FineAmount;
    private DateTime FineDate;
    private string FineReason;

    public Fines()
    {
        StudentID = 0;
        FineAmount = 0;
        FineDate = new DateTime();
        FineReason = "";
    }
    public Fines(int StudentID, double FineAmount, DateTime FineDate,string FineReason)
    {
        this.StudentID = StudentID;
        this.FineAmount = FineAmount;
        this.FineDate = FineDate;
        this.FineReason = FineReason;
    }

    public int getStudentID()
    {
        return StudentID;
    }
    public double getFineAmount()
    {
        return FineAmount;
    }
    public DateTime getFineDate()
    {
        return FineDate;
    }

    public void setStudentID(int StudentID)
    {
        this.StudentID = StudentID;
    }
    public void setFineAmount(double FineAmount)
    {
        this.FineAmount = FineAmount;
    }
    public void setFineDate(DateTime FineDate)
    {
        this.FineDate = FineDate;
    }

    public string getFineReason()
    {
        return FineReason;
    }

    public void setFineReason(string FineReason)
    {
        this.FineReason = FineReason;
    }

    public void RetrieveFineInfo(){
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuerySelect = @"
                    select * from LibraryProject.dbo.Fines;";

                SqlCommand command = new SqlCommand(sqlQuerySelect, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("-----------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine("Students ID: " + reader["StudentsID"]);
                        Console.WriteLine("Fine Amount: " + reader["FineAmount"]);
                        Console.WriteLine("FineDate: " + reader["FineDate"]);
                        Console.WriteLine("Fine Reason: " + reader["FineReason"]);
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