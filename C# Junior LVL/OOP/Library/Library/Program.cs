using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryTerminal terminal = new LibraryTerminal();

            terminal.ActivateConsole();
        }
    }

    public class LibraryTerminal
    {
        private readonly Repository _repository;

        public LibraryTerminal()
        {
            _repository = new Repository();
        }

        public void ActivateConsole()
        {
            const string AddCommand = "ADD";
            const string RemoveCommand = "REMOVE";
            const string ShowAllCommand = "SHOWALL";
            const string ShowBooksByTitleCommand = "FINDBYTITLE";
            const string ShowBooksByAuthorCommand = "FINDBYAUTHOR";
            const string ShowBooksByYearOfIssueCommand = "FINDBYYEAR";
            const string ExitCommand = "EXIT";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"\nSellect command:" +
                    $"\n{AddCommand}" +
                    $"\n{RemoveCommand}" +
                    $"\n{ShowAllCommand}" +
                    $"\n{ShowBooksByTitleCommand}" +
                    $"\n{ShowBooksByAuthorCommand}" +
                    $"\n{ShowBooksByYearOfIssueCommand}" +
                    $"\n{ExitCommand}\n");

                var userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case AddCommand:
                        Add();
                        break;
                    case RemoveCommand:
                        Remove();
                        break;
                    case ShowAllCommand:
                        ShowAll();
                        break;
                    case ShowBooksByTitleCommand:
                        ShowBooksByTitle();
                        break;
                    case ShowBooksByAuthorCommand:
                        ShowBooksByAuthor();
                        break;
                    case ShowBooksByYearOfIssueCommand:
                        ShowBooksByYearOfIssue();
                        break;
                    case ExitCommand:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Uncnown command!");
                        break;
                }
            }
        }

        private void ShowBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine(book.GetInfo());
            }
        }

        private void Add()
        {
            Console.WriteLine("Enter adding book info");

            if (Book.TryCreateBook(out Book book))
                _repository.AddBook(book);
            else
                Console.WriteLine($"Cant add book {book.GetInfo()}!");
        }

        private void Remove()
        {
            Console.WriteLine("Enter removing book info");

            if (Book.TryCreateBook(out Book removingBook) == false || _repository.TryRemoveBooks(removingBook) == false)
                Console.WriteLine($"Cant remove book {removingBook.GetInfo()}!");
        }

        private void ShowAll()
        {
            ShowBooks(_repository.GetAllBooks());
        }

        private void ShowBooksByTitle()
        {
            Console.WriteLine("Enter searching title");
            var searchingTitle = Console.ReadLine();

            ShowBooks(_repository.FindBooksByTitle(searchingTitle));
        }

        private void ShowBooksByAuthor()
        {
            Console.WriteLine("Enter searching author");
            var searchingAuthor = Console.ReadLine();

            ShowBooks(_repository.FindBooksByAuthor(searchingAuthor));
        }

        private void ShowBooksByYearOfIssue()
        {
            Console.WriteLine("Enter searching year of issue");
            if (int.TryParse(Console.ReadLine(), out int searchingYearOfIssue))
            {
                ShowBooks(_repository.FindBooksByYearOfIssue(searchingYearOfIssue));
            }
        }
    }

    public class Repository
    {
        private List<Book> _books;

        public Repository()
        {
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public bool TryRemoveBooks(Book removingBookType)
        {
            if (_books.Contains(removingBookType))
            {
                _books = _books.Where(book => book.Equals(removingBookType) == false).ToList();
                return true;
            }

            return false;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public IEnumerable<Book> FindBooksByTitle(string title)
        {
            return _books.Where(book => book.Title.ToUpper() == title.ToUpper());
        }

        public IEnumerable<Book> FindBooksByAuthor(string author)
        {
            return _books.Where(book => book.Author.ToUpper() == author.ToUpper());
        }

        public IEnumerable<Book> FindBooksByYearOfIssue(int yearOfIssue)
        {
            return _books.Where(book => book.YearOfIssue == yearOfIssue);
        }
    }

    public struct Book
    {
        private readonly static Book _emptyBook = new Book("", "", 0);

        public readonly string Title;
        public readonly string Author;

        public readonly int YearOfIssue;
    
        public Book(string title, string author, int yearOfIssue)
        {
            Title = title;
            Author = author;

            YearOfIssue = yearOfIssue;
        }

        public static bool TryCreateBook(out Book book)
        {
            Console.Write("Book title: ");
            var newBookTitle = Console.ReadLine();

            Console.Write("Book author: ");
            var newBookAuthor = Console.ReadLine();

            Console.Write("Book year of issue: ");

            if (int.TryParse(Console.ReadLine(), out int newBookYearOfIssue))
            {
                book = new Book(newBookTitle, newBookAuthor, newBookYearOfIssue);
                return true;
            }
            else
            {
                book = _emptyBook;
                return false;
            }
        }

        public string GetInfo()
        {
            return Title + " " + Author + " " + YearOfIssue;
        }
    }
}
