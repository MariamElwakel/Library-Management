using System;
using System.Data.SqlClient;

public class Section
{
    private int SectionID;
    private int NoBooks;
    private string SectionName;
    public Section()
    {
        this.SectionID = 0;
        this.NoBooks = 0;
        this.SectionName = "";
    }
    public Section(int NoBooks, string sectionName, int SectionID = 0)
    {
        this.SectionID = SectionID;
        this.NoBooks = NoBooks;
        this.SectionName = sectionName;
    }
    public void AddSectionToDatabase(Section section)
    {
        string connectionString = "Data Source=MARIAM\\MYSQLSERVER;Initial Catalog=LibraryProject;Integrated Security=True";
        string query = "INSERT INTO Sections (SectionName, NumBooks) " +
                    "VALUES (@SectionName, @NumBooks); ";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SectionName", section.SectionName);
                command.Parameters.AddWithValue("@NumBooks", section.NoBooks);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Section added successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}