using System;
using System.Data.SqlClient;
using System.Text;

namespace DB
{
    class Program
    {
        static void Main(string[] args)
        {

            bool exit = false;
            int choice;

            while(!exit){
                Console.WriteLine("Welcome to Library Management System");
                Console.WriteLine("Please enter your choice");
                Console.WriteLine("1-Student");
                Console.WriteLine("2-Admin");
                Console.WriteLine("3-Exit");

                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1){
                    bool goBack = false;
                    int options;

                    while(!goBack){
                        Console.WriteLine("Student Menu");
                        Console.WriteLine("1-Sign up");
                        Console.WriteLine("2-Login");
                        Console.WriteLine("3-Update Profile");
                        Console.WriteLine("4-Delete Profile");
                        Console.WriteLine("5-Browse Books");
                        Console.WriteLine("6-Search for a Book");
                        Console.WriteLine("7-Borrow a Book");
                        Console.WriteLine("8-Go back to Main Menu");

                        Console.Write("Enter your choice: ");
                        options = Convert.ToInt32(Console.ReadLine());

                        if (options == 1){
                            Console.Write("Enter your Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter your Password: ");
                            string password = Console.ReadLine();
                            Console.Write("Enter your Address: ");
                            string address = Console.ReadLine();
                            Console.Write("Enter your Gender: ");
                            string gender = Console.ReadLine();
                            Console.Write("Enter your Birth data: ");
                            DateTime birthDate = Convert.ToDateTime(Console.ReadLine());
                            Console.Write("Enter your Phone: ");
                            int phone = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter your Department: ");
                            string department = Console.ReadLine();
                            Student student = new Student(department,name,email,password,address,gender,phone,birthDate);
                            student.AddStuentToDatabase(student);
                        }
                        else if(options == 2){
                            Student student = new Student();
                            bool x = student.Login();
                            if(x == true){
                                Console.WriteLine("You are logged in as an Student");
                            }
                            else{
                                Console.WriteLine("There is something wrong with your email or password");
                            }
                        }
                        else if(options == 3){
                            Console.Write("Enter your ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Student student = new Student();
                            student.UpdateStudentDetails(id);
                        }
                        else if (options == 4){
                            Console.Write("Enter your ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Student student = new Student();
                            student.DeleteStudent(id);
                        }
                        else if (options == 5){
                            Student student = new Student();
                            student.browseBooks();
                        }
                        else if (options == 6){
                            Student student = new Student();
                            student.searchBook();
                        }
                        else if (options == 7){
                            Console.Write("Enter your ID: ");
                            int sID = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Book ID: ");
                            int bID = Convert.ToInt32(Console.ReadLine());
                            DateTime today = DateTime.Now;
                            Borrow borrow = new Borrow(sID,bID,today);
                            borrow.BorrowBook(borrow);
                        }
                        else if (options == 8){
                            goBack = true;
                        }
                        else{
                            Console.WriteLine("Invalid choice");
                        }
                    }
                }

                else if (choice == 2){
                    bool goBack = false;
                    int options;

                    while(!goBack){
                        Console.WriteLine("Admin Menu");
                        Console.WriteLine("1-Sign up");
                        Console.WriteLine("2-Login");
                        Console.WriteLine("3-Update Profile");
                        Console.WriteLine("4-Delete Profile");
                        Console.WriteLine("5-Browse Books");
                        Console.WriteLine("6-Search for a Book");
                        Console.WriteLine("7-Show Admins Data");
                        Console.WriteLine("8-Show Students Data");
                        Console.WriteLine("9-Add Section");
                        Console.WriteLine("10-Add Book");
                        Console.WriteLine("11-Update Book Data");
                        Console.WriteLine("12-Show Borrow Data");
                        Console.WriteLine("13-Show Fines Data");
                        Console.WriteLine("14-Give a fine to a student");
                        Console.WriteLine("15-Go back to Main Menu");

                        Console.Write("Enter your choice: ");
                        options = Convert.ToInt32(Console.ReadLine());

                        if (options == 1){
                            Console.Write("Enter your Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter your Password: ");
                            string password = Console.ReadLine();
                            Console.Write("Enter your Address: ");
                            string address = Console.ReadLine();
                            Console.Write("Enter your Gender: ");
                            string gender = Console.ReadLine();
                            Console.Write("Enter your Birth data: ");
                            DateTime birthDate = Convert.ToDateTime(Console.ReadLine());
                            Console.Write("Enter your Phone: ");
                            int phone = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Office Hours: ");
                            int officeHours = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter your Salary: ");
                            int salary = Convert.ToInt32(Console.ReadLine());
                            Staff admin = new Staff(officeHours,salary,name,email,password,address,gender,phone,birthDate);
                            admin.AddAdminToDatabase(admin);
                        }
                        else if(options == 2){
                            Staff admin = new Staff();
                            bool x = admin.Login();
                            if(x == true){
                                Console.WriteLine("You are logged in as an admin");
                            }
                            else{
                                Console.WriteLine("There is something wrong with your email or password");
                            }
                        }
                        else if(options == 3){
                            Console.Write("Enter your ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Staff admin = new Staff();
                            admin.UpdateAdminDetails(id);
                        }
                        else if (options == 4){
                            Console.Write("Enter your ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Staff admin = new Staff();
                            admin.DeleteAdmin(id);
                        }
                        else if (options == 5){
                            Staff admin = new Staff();
                            admin.browseBooks();
                        }
                        else if (options == 6){
                            Staff admin = new Staff();
                            admin.searchBook();
                        }
                        else if (options == 7){
                            Staff admin = new Staff();
                            admin.RetrieveAdminDetails();
                        }
                        else if (options == 8){
                            Staff admin = new Staff();
                            admin.RetrieveStudentDetails();
                        }
                        else if (options == 9){
                            Console.Write("Enter Section Name: ");
                            string sectionName = Console.ReadLine();
                            Console.Write("Enter Number of books: ");
                            int noBooks = Convert.ToInt32(Console.ReadLine());
                            Section section = new Section(noBooks, sectionName);
                            section.AddSectionToDatabase(section);
                        }
                        else if (options == 10){
                            Console.Write("Enter Book Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Book Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Enter Book Publisher: ");
                            string publisher = Console.ReadLine();
                            Console.Write("Enter number of copies: ");
                            int copies = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Book Quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Book Publishing Year: ");
                            int publishingYear = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Book Section: ");
                            int section = Convert.ToInt32(Console.ReadLine());

                            Book book = new Book(name, author, publisher, copies, quantity, publishingYear, section);
                            Staff admin = new Staff();
                            admin.AddBookToDatabase(book);
                        }
                        else if (options == 11){
                            Console.Write("Enter Book ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Staff admin = new Staff();
                            Book book = new Book();
                            admin.findBook(id);
                            admin.UpdateBookData(id);
                        }
                        else if (options == 12){
                            Borrow borrow = new Borrow();
                            borrow.RetrieveBorrowInfo();
                        }
                        else if (options == 13){
                            Fines fine = new Fines();
                            fine.RetrieveFineInfo(); 
                        }
                        else if (options == 14){
                            Console.Write("Enter Student ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Fine Amount: ");
                            double FineAmount = Convert.ToDouble(Console.ReadLine());
                            DateTime today = DateTime.Now;
                            Console.Write("Enter Fine Reason: ");
                            string reason = Console.ReadLine();
                            Fines fine = new Fines(id,FineAmount,today,reason);
                            Staff admin = new Staff();
                            admin.GiveFineToStudent(fine);
                        }
                        else if (options == 15){
                            goBack = true;
                        }
                        else{
                            Console.WriteLine("Invalid choice");
                        }
                    }
                }
                else if (choice == 3){
                    exit = true;
                }
            }
        }
    }
}
