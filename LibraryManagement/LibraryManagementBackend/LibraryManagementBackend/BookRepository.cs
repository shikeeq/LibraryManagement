using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace LibraryManagementBackend
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private OdbcConnection GetConnection() => new OdbcConnection(_connectionString);

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            const string sql = "SELECT book_id, category, title, press, publish_year, author, price, stock FROM book";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            BookId = Convert.ToInt32(reader["book_id"]),
                            Category = reader["category"].ToString(),
                            Title = reader["title"].ToString(),
                            Press = reader["press"].ToString(),
                            PublishYear = Convert.ToInt32(reader["publish_year"]),
                            Author = reader["author"].ToString(),
                            Price = Convert.ToDecimal(reader["price"]),
                            Stock = Convert.ToInt32(reader["stock"])
                        });
                    }
                }
            }
            return books;
        }
        public void ResetDatabase()
        {
            using (OdbcConnection connection = GetConnection())
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;


                    command.CommandText = @"
IF OBJECT_ID('dbo.borrow', 'U') IS NOT NULL DROP TABLE dbo.borrow;
IF OBJECT_ID('dbo.book', 'U') IS NOT NULL DROP TABLE dbo.book;
IF OBJECT_ID('dbo.card', 'U') IS NOT NULL DROP TABLE dbo.card;";
                    command.ExecuteNonQuery();


                    command.CommandText = @"
IF OBJECT_ID('dbo.TR_Book_ConvertEmptyStringToNull_Insert', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Book_ConvertEmptyStringToNull_Insert;
IF OBJECT_ID('dbo.TR_Book_ConvertEmptyStringToNull_Update', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Book_ConvertEmptyStringToNull_Update;";
                    command.ExecuteNonQuery();


                    command.CommandText = @"
IF OBJECT_ID('dbo.TR_Card_ConvertEmptyStringToNull_Insert', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Card_ConvertEmptyStringToNull_Insert;
IF OBJECT_ID('dbo.TR_Card_ConvertEmptyStringToNull_Update', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Card_ConvertEmptyStringToNull_Update;";
                    command.ExecuteNonQuery();

        
                    command.CommandText = @"
create table book (
    book_id int not null identity,
    category varchar(63) not null,
    title varchar(63) not null,
    press varchar(63) not null,
    publish_year int not null,
    author varchar(63) not null,
    price decimal(7, 2) not null default 0.00,
    stock int not null default 0,
    primary key (book_id),
    unique (category, press, author, title, publish_year)
);";
                    command.ExecuteNonQuery();


                    command.CommandText = @"
create table card (
    card_id int not null identity,
    name varchar(63) not null,
    department varchar(63) not null,
    type char(1) not null,
    primary key (card_id),
    unique (department, type, name),
    check ( type in ('T', 'S') )
);";
                    command.ExecuteNonQuery();

                    
                    command.CommandText = @"
create table borrow (
    card_id int not null,
    book_id int not null,
    borrow_time bigint not null,
    return_time bigint not null default 0,
    primary key (card_id, book_id, borrow_time),
    foreign key (card_id) references card(card_id) on delete cascade on update cascade,
    foreign key (book_id) references book(book_id) on delete cascade on update cascade
);";
                    command.ExecuteNonQuery();

                    
                    command.CommandText = @"
CREATE TRIGGER TR_Book_ConvertEmptyStringToNull_Insert
ON book
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO book (
        category,
        title,
        press,
        publish_year,
        author,
        price,
        stock
    )
    SELECT
        CASE WHEN TRIM(i.category) = '' THEN NULL ELSE i.category END,
        CASE WHEN TRIM(i.title) = '' THEN NULL ELSE i.title END,
        CASE WHEN TRIM(i.press) = '' THEN NULL ELSE i.press END,
        i.publish_year,
        CASE WHEN TRIM(i.author) = '' THEN NULL ELSE i.author END,
        i.price,
        i.stock
    FROM inserted AS i;
END;";
                    command.ExecuteNonQuery();

              
                    command.CommandText = @"
CREATE TRIGGER TR_Book_ConvertEmptyStringToNull_Update
ON book
INSTEAD OF UPDATE
AS
BEGIN
    UPDATE b
    SET
        category = CASE WHEN TRIM(i.category) = '' THEN NULL ELSE i.category END,
        title = CASE WHEN TRIM(i.title) = '' THEN NULL ELSE i.title END,
        press = CASE WHEN TRIM(i.press) = '' THEN NULL ELSE i.press END,
        publish_year = i.publish_year,
        author = CASE WHEN TRIM(i.author) = '' THEN NULL ELSE i.author END,
        price = i.price,
        stock = i.stock
    FROM book AS b
    INNER JOIN inserted AS i ON b.book_id = i.book_id;
END;";
                    command.ExecuteNonQuery();


                    command.CommandText = @"
CREATE TRIGGER TR_Card_ConvertEmptyStringToNull_Insert
ON card
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO card (
        name,
        department,
        type
    )
    SELECT
        CASE WHEN TRIM(i.name) = '' THEN NULL ELSE i.name END,
        CASE WHEN TRIM(i.department) = '' THEN NULL ELSE i.department END,
        CASE WHEN TRIM(i.type) = '' THEN NULL ELSE i.type END
    FROM inserted AS i;
END;";
                    command.ExecuteNonQuery();

                 
                    command.CommandText = @"
CREATE TRIGGER TR_Card_ConvertEmptyStringToNull_Update
ON card
INSTEAD OF UPDATE
AS
BEGIN
    UPDATE c
    SET
        name = CASE WHEN TRIM(i.name) = '' THEN NULL ELSE i.name END,
        department = CASE WHEN TRIM(i.department) = '' THEN NULL ELSE i.department END,
        type = CASE WHEN TRIM(i.type) = '' THEN NULL ELSE i.type END
    FROM card AS c
    INNER JOIN inserted AS i ON c.card_id = i.card_id;
END;";
                    command.ExecuteNonQuery();

            
                    command.CommandText = @"
INSERT INTO book (category, title, press, publish_year, author, price, stock) VALUES
('Fiction', 'One Hundred Years of Solitude', 'Harper Perennial', 1967, 'Gabriel Garcia Marquez', 35.00, 10),
('Science Fiction', 'The Three-Body Problem', 'Tor Books', 2014, 'Liu Cixin', 45.00, 5),
('History', 'Sapiens: A Brief History of Humankind', 'Harper', 2014, 'Yuval Noah Harari', 55.00, 8),
('Programming', 'C# in Depth', 'Manning Publications', 2019, 'Jon Skeet', 78.50, 3),
('Programming', 'SQL QuickStart Guide', 'ClydeBank Media LLC', 2019, 'Walter Shields', 42.00, 7),
('Fiction', 'To Kill a Mockingbird', 'J. B. Lippincott & Co.', 1960, 'Harper Lee', 28.00, 12),
('Science Fiction', 'Foundation', 'Gnome Press', 1951, 'Isaac Asimov', 38.00, 6);";
                    command.ExecuteNonQuery();

        
                    command.CommandText = @"
INSERT INTO card (name, department, type) VALUES
('John Smith', 'Computer Science', 'S'),
('Jane Doe', 'Electrical Engineering', 'S'),
('Peter Jones', 'Mathematics', 'T'),
('Mary Brown', 'Physics', 'T'),
('David Wilson', 'Chemical Engineering', 'S');";
                    command.ExecuteNonQuery();



                    Console.WriteLine("资料库初始化成功！");
                }
            }
        }

        public Book GetBookById(int bookId)
        {
            const string sql = "SELECT book_id, category, title, press, publish_year, author, price, stock FROM book WHERE book_id = ?";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                command.Parameters.AddWithValue("?", bookId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Book
                        {
                            BookId = Convert.ToInt32(reader["book_id"]),
                            Category = reader["category"].ToString(),
                            Title = reader["title"].ToString(),
                            Press = reader["press"].ToString(),
                            PublishYear = Convert.ToInt32(reader["publish_year"]),
                            Author = reader["author"].ToString(),
                            Price = Convert.ToDecimal(reader["price"]),
                            Stock = Convert.ToInt32(reader["stock"])
                        };
                    }
                }
            }
            return null;
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            var result = new List<Book>();
            const string sql = "SELECT * FROM book WHERE title LIKE ?";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                command.Parameters.AddWithValue("?", "%" + title + "%");
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Book
                        {
                            BookId = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            Title = reader.GetString(2),
                            Press = reader.GetString(3),
                            PublishYear = reader.GetInt32(4),
                            Author = reader.GetString(5),
                            Price = reader.GetDecimal(6),
                            Stock = reader.GetInt32(7)
                        });
                    }
                }
            }
            return result;
        }

        public List<Book> SearchBooksByAuthor(string author)
        {
            var result = new List<Book>();
            const string sql = "SELECT * FROM book WHERE author LIKE ?";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                command.Parameters.AddWithValue("?", "%" + author + "%");
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Book
                        {
                            BookId = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            Title = reader.GetString(2),
                            Press = reader.GetString(3),
                            PublishYear = reader.GetInt32(4),
                            Author = reader.GetString(5),
                            Price = reader.GetDecimal(6),
                            Stock = reader.GetInt32(7)
                        });
                    }
                }
            }
            return result;
        }

        public void AddBook(Book book)
        {
            const string sql = "INSERT INTO book (category, title, press, publish_year, author, price, stock) VALUES (?, ?, ?, ?, ?, ?, ?)";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                command.Parameters.AddWithValue("?", book.Category);
                command.Parameters.AddWithValue("?", book.Title);
                command.Parameters.AddWithValue("?", book.Press);
                command.Parameters.AddWithValue("?", book.PublishYear);
                command.Parameters.AddWithValue("?", book.Author);
                command.Parameters.AddWithValue("?", book.Price);
                command.Parameters.AddWithValue("?", book.Stock);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (OdbcException ex)
                {
                    if (ex.Errors.Count > 0 && ex.Errors[0].SQLState == "23000")
                        throw new InvalidOperationException("无法创建重复的书籍，请检查书籍的唯一性。");
                    throw;
                }
            }
        }

        public void AddBooks(List<Book> books)
        {
            if (books == null || books.Count == 0) return;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var book in books)
                        {
                            using (var command = new OdbcCommand(
                                "INSERT INTO book (category, title, press, publish_year, author, price, stock) VALUES (?, ?, ?, ?, ?, ?, ?)",
                                connection, transaction))
                            {
                                command.Parameters.AddWithValue("?", book.Category);
                                command.Parameters.AddWithValue("?", book.Title);
                                command.Parameters.AddWithValue("?", book.Press);
                                command.Parameters.AddWithValue("?", book.PublishYear);
                                command.Parameters.AddWithValue("?", book.Author);
                                command.Parameters.AddWithValue("?", book.Price);
                                command.Parameters.AddWithValue("?", book.Stock);
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (OdbcException ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("批量导入失败：" + ex.Message, ex);
                    }
                }
            }
        }

        public void UpdateBook(Book book)
        {
            const string sql = "UPDATE book SET category = ?, title = ?, press = ?, publish_year = ?, author = ?, price = ?, stock = ? WHERE book_id = ?";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                command.Parameters.AddWithValue("?", book.Category);
                command.Parameters.AddWithValue("?", book.Title);
                command.Parameters.AddWithValue("?", book.Press);
                command.Parameters.AddWithValue("?", book.PublishYear);
                command.Parameters.AddWithValue("?", book.Author);
                command.Parameters.AddWithValue("?", book.Price);
                command.Parameters.AddWithValue("?", book.Stock);
                command.Parameters.AddWithValue("?", book.BookId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int bookId)
        {
            const string sql = "DELETE FROM book WHERE book_id = ?";
            using (var connection = GetConnection())
            using (var command = new OdbcCommand(sql, connection))
            {
                command.Parameters.AddWithValue("?", bookId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}