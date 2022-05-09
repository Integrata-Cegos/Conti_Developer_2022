namespace Javacream.Books
{

    public class Publisher
    {
        public string Name { get; }
        private List<Book> _books;

        public Publisher(string name)
        {
            this.Name = name;
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            this._books.Add(book);
        }
        public void RemoveBook(Book book)
        {
            this._books.Remove(book);

        }
        public void ClearBooks()
        {
            this._books.Clear();
        }
        public List<Book> GetAllBooks()
        {
            return this._books;
        }

    }
    public class Isbn
    {
        private int _part1;
        private int _part2;
        private int _part3;
        private int _part4;

        public Isbn(int p1, int p2, int p3, int p4)
        {
            this._part1 = p1;
            this._part2 = p2;
            this._part3 = p3;
            this._part4 = p4;

        }
        public override int GetHashCode()
        {
            //Prinzipiell OK, aber führt zu Performance-Problemen
            //return 0;
            //Gut unter Annahme, dass jeder Part nur den Wertebereich 1-999 haben kann
            return 1000000000 * _part1 + 1000000 * _part2 + 1000 * _part3 + _part4;
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                if (this == obj)
                {
                    return true;
                }
                if (obj.GetType != this.GetType)
                {
                    return false;
                }

                Isbn toCompare = (Isbn)obj;

                return this._part1 == toCompare._part1 && this._part2 == toCompare._part2 && this._part3 == toCompare._part3 && this._part4 == toCompare._part4;
            }
        }


        public override string ToString()
        {
            string isbnAsString = "ISBN:" + _part1 + "-" + _part2 + "-" + _part3 + "-" + _part4;
            return isbnAsString;
        }

    }
    public class BooksService
    {
        private Dictionary<Isbn, Book> _books = new Dictionary<Isbn, Book>();
        public Book CreateBook(Isbn isbn, string title, int pages, double price, bool available, Dictionary<string, Object> options)
        {
            Book newBook;
            try
            {
            string? topic = options["topic"].ToString();
                newBook = new SpecialistBook(isbn, title, pages, price, available, topic!);
            }
            catch (Exception)
            {
                try
                {
                    string? subject = options["subject"].ToString();
                    int year = (int)options["year"];
                    newBook = new SchoolBook(isbn, title, pages, price, available, year, subject!);
                }
                catch (Exception)
                {
                    newBook = new Book(isbn, title, pages, price, available);
                }
            }
            this._books.Add(isbn, newBook);
            return newBook;
        }

        public Book FindBookByIsbn(Isbn isbn)
        {
            return this._books[isbn];
        }
        public void DeleteBookByIsbn(Isbn isbn)
        {
            this._books.Remove(isbn);
        }

    }
    public class Book
    {
        public Isbn Isbn { get; }
        private string? _title;
        public string Title
        {
            get
            {
                return _title!;
            }
            private set
            {
                if (value.Length >= 2)
                {
                    this._title = value;
                }
                else
                {
                    throw new Exception("invalid title: " + value);
                }
            }
        }
        private int _pages;
        public int Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                if (value > 0)
                {
                    this._pages = value;
                }
                else
                {
                    throw new Exception("invalid pages: " + value);
                }
            }
        }

        public bool Available { get; set; }

        private double _price;

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value >= 0)
                {
                    this._price = value;
                }
                else
                {
                    throw new Exception("invalid price: " + value);
                }

            }
        }

        public Book(Isbn isbn, string title, int pages, double price, bool available)
        {
            this.Isbn = isbn;
            this.Title = title;
            this.Pages = pages;
            this.Price = price;
            this.Available = available;
        }

        public virtual string Info()
        {
            return "Book: isbn=" + Isbn.ToString() + ", title=" + Title + ", pages=" + Pages + ", price=" + Price + ", available=" + Available;
        }
    }

    public class SchoolBook : Book
    {
        public int Year { get; }
        public string Subject { get; }

        public SchoolBook(Isbn isbn, string title, int pages, double price, bool available, int year, string subject) : base(isbn, title, pages, price, available)
        {
            this.Year = year;
            this.Subject = subject;
        }
        public override string Info()
        {
            return base.Info() + ", year=" + Year + ", subject=" + Subject;
        }

    }

    public class SpecialistBook : Book
    {
        public string Topic { get; }

        public SpecialistBook(Isbn isbn, string title, int pages, double price, bool available, string topic) : base(isbn, title, pages, price, available)
        {
            this.Topic = topic;
        }
        public override string Info()
        {
            return base.Info() + ", topic=" + Topic;
        }

    }

}