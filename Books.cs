using System;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;

public class Book
{
    private int ISBN;
    private int availableQuantity;
    private int PublishingYear;
    private int NoCopies;
    private string Author;
    private string BookName;
    private string Publisher;
    private int sectionID;
    public Book(string BookName,string Author,string Publisher,int NoCopies,int AvailableQuantity,int PublishingYear,int sectionID, int ISBN = 0)
    {
        this.ISBN=ISBN;
        this.PublishingYear=PublishingYear;
        this.NoCopies=NoCopies;
        this.Author=Author;
        this.BookName=BookName;
        this.Publisher=Publisher;
        this.availableQuantity = AvailableQuantity;
        this.sectionID = sectionID;
    }
    public Book() {
        this.ISBN = 0;
        this.availableQuantity = 0;
        this.PublishingYear = 0;
        this.NoCopies = 0;
        this.Author = "";
        this.BookName = "";
        this.Publisher = "";
        this.sectionID = 0;
    }
    public int getAvailableQuantity()
    {
        return availableQuantity;
    }
    public int getPublishingYear()
    {
        return PublishingYear;
    }
    public int getNoCopies()
    {
        return NoCopies;
    }
    public string getAuthor()
    {
        return Author;
    }
    public string getBookName()
    {
        return BookName;
    }
    public string getPublisher()
    {
        return Publisher;
    }
    public int GetSection()
    {
        return sectionID;
    }
    
}
