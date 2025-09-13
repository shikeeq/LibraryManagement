using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Odbc;

namespace LibraryManagementBackend
{
    internal class Program
    {
        static OdbcConnection GetDatabaseConnection(string connectionString)
        {
            OdbcConnection connection = new OdbcConnection(connectionString);
            int retryCount = 0;
            int maxRetries = 3; // 设置最大重试次数

            while (retryCount < maxRetries)
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("成功连接到数据库！");
                    return connection;
                }
                catch (OdbcException ex)
                {
                    Console.WriteLine($"连接数据库失败 (第 {retryCount + 1} 次尝试)：{ex.Message}");
                    retryCount++;
                    if (retryCount < maxRetries)
                    {
                        Console.Write("是否要重试连接？ (y/n): ");
                        string retryInput = Console.ReadLine()?.ToLower();
                        if (retryInput != "y")
                        {
                            return null; // 用户选择不重试
                        }
                        // 可以加入等待时间，例如 System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            Console.WriteLine("达到最大重试次数，连接失败。");
            return null;
        }

        static void Main(string[] args)
        {
            string connectionString = "Driver={ODBC Driver 17 for SQL Server};Server=LAPTOP-476JT8H0\\MSSQLSERVER_11;Database=db00;Trusted_Connection=Yes;";

            using (OdbcConnection connection = GetDatabaseConnection(connectionString))
            {
                if (connection != null)
                {
                    Console.Write("是否要初始化数据库 (这会删除现有数据表)? (输入 y 初始化，按 Enter 或其他不初始化): ");
                    string initializeInput = Console.ReadLine()?.ToLower();

                    if (initializeInput == "y")
                    {
                        using (OdbcConnection initConnection = new OdbcConnection(connectionString))
                        {
                            try
                            {
                                initConnection.Open();
                                using (OdbcCommand command = new OdbcCommand())
                                {
                                    command.Connection = connection;

                                    // 批次 1: 删除现有表格
                                    command.CommandText = @"
IF OBJECT_ID('dbo.borrow', 'U') IS NOT NULL DROP TABLE dbo.borrow;
IF OBJECT_ID('dbo.book', 'U') IS NOT NULL DROP TABLE dbo.book;
IF OBJECT_ID('dbo.card', 'U') IS NOT NULL DROP TABLE dbo.card;";
                                    command.ExecuteNonQuery();

                                    // 批次 2: 删除 book 表的触发器
                                    command.CommandText = @"
IF OBJECT_ID('dbo.TR_Book_ConvertEmptyStringToNull_Insert', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Book_ConvertEmptyStringToNull_Insert;
IF OBJECT_ID('dbo.TR_Book_ConvertEmptyStringToNull_Update', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Book_ConvertEmptyStringToNull_Update;";
                                    command.ExecuteNonQuery();

                                    // 批次 3: 删除 card 表的触发器
                                    command.CommandText = @"
IF OBJECT_ID('dbo.TR_Card_ConvertEmptyStringToNull_Insert', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Card_ConvertEmptyStringToNull_Insert;
IF OBJECT_ID('dbo.TR_Card_ConvertEmptyStringToNull_Update', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_Card_ConvertEmptyStringToNull_Update;";
                                    command.ExecuteNonQuery();

                                    // 批次 4: 创建 book 表
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

                                    // 批次 5: 创建 card 表
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

                                    // 批次 6: 创建 borrow 表
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

                                    // 批次 7: 创建 book 表的 INSERT 触发器
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

                                    // 批次 8: 创建 book 表的 UPDATE 触发器
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

                                    // 批次 9: 创建 card 表的 INSERT 触发器
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

                                    // 批次 10: 创建 card 表的 UPDATE 触发器
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

                                    // 批次 11: 插入 book 的示例数据
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

                                    // 批次 12: 插入 card 的示例数据
                                    command.CommandText = @"
INSERT INTO card (name, department, type) VALUES
('John Smith', 'Computer Science', 'S'),
('Jane Doe', 'Electrical Engineering', 'S'),
('Peter Jones', 'Mathematics', 'T'),
('Mary Brown', 'Physics', 'T'),
('David Wilson', 'Chemical Engineering', 'S');";
                                    command.ExecuteNonQuery();

                                    // 批次 13: 插入 borrow 的示例数据: 初始应没有借阅记录
                                    //                                    command.CommandText = @"
                                    //INSERT INTO borrow (card_id, book_id, borrow_time, return_time) VALUES
                                    //(1, 1, 1672531200, 0),
                                    //(2, 2, 1675209600, 1677801600),
                                    //(3, 4, 1677888000, 0),
                                    //(1, 5, 1680393600, 1683072000),
                                    //(4, 3, 1683158400, 0);";
                                    //                                    command.ExecuteNonQuery();

                                    Console.WriteLine("数据库初始化成功！");
                                }
                            }
                            catch (OdbcException ex)
                            {
                                Console.WriteLine($"数据库初始化失败：{ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("跳过数据库初始化。");
                    }

                    BookRepository bookRepository = new BookRepository(connectionString);
                    CardRepository cardRepository = new CardRepository(connectionString);
                    BorrowRepository borrowRepository = new BorrowRepository(connectionString);

                    // CLI 菜单
                    string choice;
                    do
                    {
                        Console.WriteLine("\n请选择要测试的功能：");
                        Console.WriteLine("1. 输出所有书籍");
                        Console.WriteLine("2. 输出所有借书证");
                        Console.WriteLine("3. 输出所有借阅记录");
                        Console.WriteLine("4. 管理书籍");
                        Console.WriteLine("5. 管理借书证");
                        Console.WriteLine("6. 管理借阅");
                        Console.WriteLine("x. 退出");
                        Console.Write("请输入您的选择：");
                        choice = Console.ReadLine()?.ToLower();

                        switch (choice)
                        {
                            case "1":
                                TestBookRepository(bookRepository);
                                break;
                            case "2":
                                TestCardRepository(cardRepository);
                                break;
                            case "3":
                                TestBorrowRepository(borrowRepository);
                                break;
                            case "4":
                                ManageBooks(bookRepository);
                                break;
                            case "5":
                                ManageCards(cardRepository);
                                break;
                            case "6":
                                ManageBorrows(borrowRepository, bookRepository);
                                break;
                            case "x":
                                Console.WriteLine("程序结束，按任意键退出。");
                                break;
                            default:
                                Console.WriteLine("无效的选择，请重新输入。");
                                break;
                        }
                    } while (choice != "x");
                }
                else
                {
                    Console.WriteLine("无法建立数据库连接，程序终止。");
                }
            }

            Console.ReadKey(); // 让主控台窗口保持开启 (在菜单退出后)
        }

        static void TestBookRepository(BookRepository bookRepository)
        {
            // Console.WriteLine("\n测试 BookRepository:");
            // 取得所有书籍
            Console.WriteLine("所有书籍：");
            List<Book> allBooks = bookRepository.GetAllBooks();
            foreach (var book in allBooks)
            {
                Console.WriteLine($"ID: {book.BookId}, 标题: {book.Title}, 作者: {book.Author}, 库存: {book.Stock}");
            }

            Console.WriteLine();

            // 根据 ID 取得单本书籍
            //int bookIdToFind = 1; // 假设你要查询 ID 为 1 的书籍
            //Book foundBook = bookRepository.GetBookById(bookIdToFind);
            //if (foundBook != null)
            //{
            //    Console.WriteLine($"ID 为 {bookIdToFind} 的书籍：标题: {foundBook.Title}, 作者: {foundBook.Author}");
            //}
            //else
            //{
            //    Console.WriteLine($"找不到 ID 为 {bookIdToFind} 的书籍。");
            //}

            //Console.WriteLine();

            // 新增书籍
            //Book newBook = new Book
            //{
            //    Category = "测试分类",
            //    Title = "测试书名",
            //    Press = "测试出版社",
            //    PublishYear = 2023,
            //    Author = "测试作者",
            //    Price = 19.99m,
            //    Stock = 5
            //};
            // bookRepository.AddBook(newBook);
            // Console.WriteLine("已新增一本新书籍。");
        }

        static void TestCardRepository(CardRepository cardRepository)
        {
            // Console.WriteLine("\n输出所有借书证:");
            // 取得所有借书证
            Console.WriteLine("所有借书证：");
            List<Card> allCards = cardRepository.GetAllCards();
            foreach (var card in allCards)
            {
                Console.WriteLine($"ID: {card.CardId}, 姓名: {card.Name}, 部门: {card.Department}, 类型: {card.Type}");
            }

            Console.WriteLine();

            // 新增借书证
            //Card newCard = new Card
            //{
            //    Name = "测试姓名",
            //    Department = "测试部门",
            //    Type = 'T'
            //};
            // cardRepository.AddCard(newCard);
            // Console.WriteLine("已新增一张借书证。");
        }

        static void TestBorrowRepository(BorrowRepository borrowRepository)
        {
            // Console.WriteLine("\n输出所有借阅记录:");
            // 取得所有借阅记录
            Console.WriteLine("所有借阅记录：");
            List<Borrow> allBorrows = borrowRepository.GetAllBorrows();
            foreach (var borrow in allBorrows)
            {
                DateTime borrowTime = DateTimeOffset.FromUnixTimeSeconds(borrow.BorrowTime).LocalDateTime;
                DateTime returnTime = DateTimeOffset.FromUnixTimeSeconds(borrow.ReturnTime).LocalDateTime;
                string returnTimeString = borrow.ReturnTime == 0
                    ? "尚未归还"
                    : DateTimeOffset.FromUnixTimeSeconds(borrow.ReturnTime).LocalDateTime.ToString();
                Console.WriteLine(
                    $"借书证 ID: {borrow.CardId}, 书籍 ID: {borrow.BookId}, 借阅时间: {borrowTime}, 归还时间: {returnTimeString}");
            }

            Console.WriteLine();

            // 新增借阅记录 (需要提供有效的 CardId 和 BookId)
            //Borrow newBorrow = new Borrow
            //{
            //    CardId = 1, // 请确保数据库中有 CardId 为 1 的记录
            //    BookId = 1, // 请确保数据库中有 BookId 为 1 的记录
            //    BorrowTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
            //    ReturnTime = 0
            //};
            // borrowRepository.AddBorrow(newBorrow);
            // Console.WriteLine("已新增一条借阅记录。");
        }

        // 新增 ManageBooks 方法
        static void ManageBooks(BookRepository bookRepository)
        {
            string bookChoice;
            do
            {
                Console.WriteLine("\n书籍管理：");
                Console.WriteLine("1. 查看所有书籍");
                Console.WriteLine("2. 根据 ID 查看书籍");
                Console.WriteLine("3. 新增书籍");
                Console.WriteLine("4. 更新书籍");
                Console.WriteLine("5. 删除书籍");
                Console.WriteLine("b. 返回上一层");
                Console.Write("请输入您的选择：");
                bookChoice = Console.ReadLine()?.ToLower();

                switch (bookChoice)
                {
                    case "1":
                        TestBookRepository(bookRepository); // 沿用之前的测试方法来查看所有书籍
                        break;
                    case "2":
                        Console.Write("请输入要查看的书籍 ID：");
                        if (int.TryParse(Console.ReadLine(), out int bookIdToFind))
                        {
                            Book foundBook = bookRepository.GetBookById(bookIdToFind);
                            if (foundBook != null)
                            {
                                Console.WriteLine(
                                    $"书籍 ID: {foundBook.BookId}, 类别: {foundBook.Category}, 标题: {foundBook.Title}, 作者: {foundBook.Author}, 出版年份: {foundBook.PublishYear}, 出版社: {foundBook.Press}, 价格: {foundBook.Price}, 库存: {foundBook.Stock}");
                            }
                            else
                            {
                                Console.WriteLine($"找不到 ID 为 {bookIdToFind} 的书籍。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("输入的 ID 格式不正确。");
                        }

                        break;
                    case "3":
                        // 提示用户输入书籍的各个属性，然后创建 Book 对象并调用 bookRepository.AddBook 方法
                        Console.WriteLine("请输入书籍信息：");
                        Console.Write("类别：");
                        string category = Console.ReadLine();
                        Console.Write("标题：");
                        string title = Console.ReadLine();
                        Console.Write("出版社：");
                        string press = Console.ReadLine();
                        Console.Write("出版年份：");
                        if (int.TryParse(Console.ReadLine(), out int publishYear))
                        {
                            Console.Write("作者：");
                            string author = Console.ReadLine();
                            Console.Write("价格：");
                            if (decimal.TryParse(Console.ReadLine(), out decimal price))
                            {
                                Console.Write("库存：");
                                if (int.TryParse(Console.ReadLine(), out int stock))
                                {
                                    Book newBook = new Book
                                    {
                                        Category = category,
                                        Title = title,
                                        Press = press,
                                        PublishYear = publishYear,
                                        Author = author,
                                        Price = price,
                                        Stock = stock
                                    };
                                    try
                                    {
                                        bookRepository.AddBook(newBook);
                                        Console.WriteLine("书籍已成功新增。");
                                    }
                                    catch (OdbcException ex)
                                    {
                                        // 检查是否为 NOT NULL 约束错误
                                        if (ex.Message.Contains("Cannot insert the value NULL into column 'category'") ||
                                            ex.Message.Contains("Cannot insert the value NULL into column 'title'") ||
                                            ex.Message.Contains("Cannot insert the value NULL into column 'press'") ||
                                            ex.Message.Contains("Cannot insert the value NULL into column 'author'"))
                                        {
                                            Console.WriteLine("新增书籍失败：类别、标题、出版社和作者不能为空。请重新输入。");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"新增书籍失败：{ex.Message}");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("库存格式不正确。");
                                }
                            }
                            else
                            {
                                Console.WriteLine("价格格式不正确。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("出版年份格式不正确。");
                        }

                        break;
                    case "4":
                        // 提示用户输入要更新的书籍 ID 和新的属性值，然后创建 Book 对象并调用 bookRepository.UpdateBook 方法
                        Console.Write("请输入要更新的书籍 ID：");
                        if (int.TryParse(Console.ReadLine(), out int bookIdToUpdate))
                        {
                            Book existingBook = bookRepository.GetBookById(bookIdToUpdate);
                            if (existingBook != null)
                            {
                                Console.WriteLine("请输入新的书籍信息 (留空表示不修改)：");
                                Console.Write($"类别 ({existingBook.Category})：");
                                string newCategory = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newCategory)) existingBook.Category = newCategory;
                                // ... (类似地提示并更新其他属性)
                                Console.Write($"标题 ({existingBook.Title})：");
                                string newTitle = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newTitle)) existingBook.Title = newTitle;
                                // ... (提示并更新其他属性，例如出版社、出版年份、作者、价格、库存)

                                Console.Write($"出版年份 ({existingBook.PublishYear})：");
                                if (int.TryParse(Console.ReadLine(), out int newPublishYear))
                                    existingBook.PublishYear = newPublishYear;

                                Console.Write($"出版社 ({existingBook.Press})：");
                                string newPress = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newPress)) existingBook.Press = newPress;

                                Console.Write($"作者 ({existingBook.Author})：");
                                string newAuthor = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newAuthor)) existingBook.Author = newAuthor;

                                Console.Write($"价格 ({existingBook.Price})：");
                                if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                                    existingBook.Price = newPrice;

                                Console.Write($"库存 ({existingBook.Stock})：");
                                if (int.TryParse(Console.ReadLine(), out int newStock)) existingBook.Stock = newStock;

                                bookRepository.UpdateBook(existingBook);
                                Console.WriteLine("书籍信息已成功更新。");
                            }
                            else
                            {
                                Console.WriteLine($"找不到 ID 为 {bookIdToUpdate} 的书籍。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("输入的 ID 格式不正确。");
                        }

                        break;
                    case "5":
                        Console.Write("请输入要删除的书籍 ID：");
                        if (int.TryParse(Console.ReadLine(), out int bookIdToDelete))
                        {
                            // 检查书籍是否存在
                            Book bookToDelete = bookRepository.GetBookById(bookIdToDelete);
                            if (bookToDelete != null)
                            {
                                bookRepository.DeleteBook(bookIdToDelete);
                                Console.WriteLine($"ID 为 {bookIdToDelete} 的书籍已成功删除。");
                            }
                            else
                            {
                                Console.WriteLine($"ID 为 {bookIdToDelete} 的书籍不存在，无法删除。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("输入的 ID 格式不正确。");
                        }

                        break;
                    case "b":
                        break;
                    default:
                        Console.WriteLine("无效的选择，请重新输入。");
                        break;
                }
            } while (bookChoice != "b");
        }

        static void ManageCards(CardRepository cardRepository)
        {
            string cardChoice;
            do
            {
                Console.WriteLine("\n借书证管理：");
                Console.WriteLine("1. 查看所有借书证");
                Console.WriteLine("2. 根据 ID 查看借书证");
                Console.WriteLine("3. 新增借书证");
                Console.WriteLine("4. 更新借书证");
                Console.WriteLine("5. 删除借书证");
                Console.WriteLine("b. 返回上一层");
                Console.Write("请输入您的选择：");
                cardChoice = Console.ReadLine()?.ToLower();

                switch (cardChoice)
                {
                    case "1":
                        Console.WriteLine("\n所有借书证：");
                        List<Card> allCards = cardRepository.GetAllCards();
                        foreach (var card in allCards)
                        {
                            Console.WriteLine(
                                $"ID: {card.CardId}, 姓名: {card.Name}, 部门: {card.Department}, 类型: {card.Type}");
                        }

                        Console.WriteLine();
                        break;
                    case "2":
                        Console.Write("请输入要查看的借书证 ID：");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToFind))
                        {
                            Card foundCard = cardRepository.GetCardById(cardIdToFind);
                            if (foundCard != null)
                            {
                                Console.WriteLine(
                                    $"借书证 ID: {foundCard.CardId}, 姓名: {foundCard.Name}, 部门: {foundCard.Department}, 类型: {foundCard.Type}");
                            }
                            else
                            {
                                Console.WriteLine($"找不到 ID 为 {cardIdToFind} 的借书证。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("输入的 ID 格式不正确。");
                        }

                        break;
                    case "3":
                        Console.WriteLine("请输入借书证信息：");
                        Console.Write("姓名：");
                        string name = Console.ReadLine();
                        Console.Write("部门：");
                        string department = Console.ReadLine();
                        Console.Write("类型 (T/S)：");
                        string typeInput = Console.ReadLine()?.ToUpper();
                        if (typeInput == "T" || typeInput == "S")
                        {
                            Card newCard = new Card
                            {
                                Name = name,
                                Department = department,
                                Type = typeInput[0]
                            };
                            try
                            {
                                cardRepository.AddCard(newCard);
                                Console.WriteLine("借书证已成功新增。");
                            }
                            catch (OdbcException ex)
                            {
                                // 检查是否为 NOT NULL 约束错误
                                if (ex.Message.Contains("Cannot insert the value NULL into column 'name'") ||
                                    ex.Message.Contains("Cannot insert the value NULL into column 'department'") ||
                                    ex.Message.Contains("Cannot insert the value NULL into column 'type'"))
                                {
                                    Console.WriteLine("新增借书证失败：姓名、部门和类型不能为空。请重新输入。");
                                }
                                else
                                {
                                    Console.WriteLine($"新增借书证失败：{ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("类型输入不正确，请输入 'T' 或 'S'。");
                        }

                        break;
                    case "4":
                        Console.Write("请输入要更新的借书证 ID：");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToUpdate))
                        {
                            Card existingCard = cardRepository.GetCardById(cardIdToUpdate);
                            if (existingCard != null)
                            {
                                Console.WriteLine("请输入新的借书证信息 (留空表示不修改)：");
                                Console.Write($"姓名 ({existingCard.Name})：");
                                string newName = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newName)) existingCard.Name = newName;

                                Console.Write($"部门 ({existingCard.Department})：");
                                string newDepartment = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newDepartment)) existingCard.Department = newDepartment;

                                Console.Write($"类型 ({existingCard.Type}) (T/S)：");
                                string newTypeInput = Console.ReadLine()?.ToUpper();
                                if (!string.IsNullOrEmpty(newTypeInput) &&
                                    (newTypeInput == "T" || newTypeInput == "S"))
                                {
                                    existingCard.Type = newTypeInput[0];
                                }

                                cardRepository.UpdateCard(existingCard);
                                Console.WriteLine("借书证信息已成功更新。");
                            }
                            else
                            {
                                Console.WriteLine($"找不到 ID 为 {cardIdToUpdate} 的借书证。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("输入的 ID 格式不正确。");
                        }

                        break;
                    case "5":
                        Console.Write("请输入要删除的借书证 ID：");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToDelete))
                        {
                            // 检查借书证是否存在
                            Card cardToDelete = cardRepository.GetCardById(cardIdToDelete);
                            if (cardToDelete != null)
                            {
                                cardRepository.DeleteCard(cardIdToDelete);
                                Console.WriteLine($"ID 为 {cardIdToDelete} 的借书证已成功删除。");
                            }
                            else
                            {
                                Console.WriteLine($"ID 为 {cardIdToDelete} 的借书证不存在，无法删除。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("输入的 ID 格式不正确。");
                        }

                        break;
                    case "b":
                        break;
                    default:
                        Console.WriteLine("无效的选择，请重新输入。");
                        break;
                }
            } while (cardChoice != "b");
        }

        static void ManageBorrows(BorrowRepository borrowRepository, BookRepository bookRepository)
        {
            string borrowChoice;
            do
            {
                Console.WriteLine("\n借阅管理：");
                Console.WriteLine("1. 查看所有借阅记录");
                Console.WriteLine("2. 借阅书籍");
                Console.WriteLine("3. 归还书籍");
                Console.WriteLine("b. 返回上一层");
                Console.Write("请输入您的选择：");
                borrowChoice = Console.ReadLine()?.ToLower();

                switch (borrowChoice)
                {
                    case "1":
                        Console.WriteLine("\n所有借阅记录：");
                        List<Borrow> allBorrows = borrowRepository.GetAllBorrows();
                        foreach (var borrow in allBorrows)
                        {
                            DateTime borrowTime =
                                DateTimeOffset.FromUnixTimeSeconds(borrow.BorrowTime).LocalDateTime;
                            DateTime returnTime =
                                DateTimeOffset.FromUnixTimeSeconds(borrow.ReturnTime).LocalDateTime;
                            Console.WriteLine(
                                $"借书证 ID: {borrow.CardId}, 书籍 ID: {borrow.BookId}, 借阅时间: {borrowTime}, 归还时间: {(borrow.ReturnTime == 0 ? "尚未归还" : returnTime.ToString())}");
                        }

                        Console.WriteLine();
                        break;
                    case "2":
                        Console.Write("请输入借书证 ID：");
                        if (int.TryParse(Console.ReadLine(), out int borrowCardId))
                        {
                            Console.Write("请输入要借阅的书籍 ID：");
                            if (int.TryParse(Console.ReadLine(), out int borrowBookId))
                            {
                                // 检查是否已经有未归还的记录
                                Borrow existingBorrow = borrowRepository.GetBorrowByCardAndBookId(borrowCardId, borrowBookId);
                                if (existingBorrow != null && existingBorrow.ReturnTime == 0)
                                {
                                    Console.WriteLine($"借阅失败：借书证 ID {borrowCardId} 已经借阅了书籍 ID {borrowBookId}，且尚未归还。");
                                }
                                else
                                {
                                    Book bookToBorrow = bookRepository.GetBookById(borrowBookId);
                                    if (bookToBorrow != null && bookToBorrow.Stock > 0)
                                    {
                                        Borrow newBorrow = new Borrow
                                        {
                                            CardId = borrowCardId,
                                            BookId = borrowBookId,
                                            BorrowTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                                            ReturnTime = 0
                                        };
                                        try
                                        {
                                            borrowRepository.AddBorrow(newBorrow);
                                            bookToBorrow.Stock--;
                                            bookRepository.UpdateBook(bookToBorrow); // 更新书籍库存
                                            Console.WriteLine("书籍借阅成功。");
                                        }
                                        catch (OdbcException ex)
                                        {
                                            Console.WriteLine($"借阅失败：{ex.Message}");
                                        }
                                    }
                                    else if (bookToBorrow == null)
                                    {
                                        Console.WriteLine($"找不到 ID 为 {borrowBookId} 的书籍。");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"ID 为 {borrowBookId} 的书籍目前没有库存。");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("书籍 ID 格式不正确。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("借书证 ID 格式不正确。");
                        }

                        break;
                    case "3":
                        Console.Write("请输入归还书籍的借书证 ID：");
                        if (int.TryParse(Console.ReadLine(), out int returnCardId))
                        {
                            Console.Write("请输入要归还的书籍 ID：");
                            if (int.TryParse(Console.ReadLine(), out int returnBookId))
                            {
                                // 检查是否有未归还的记录
                                Borrow existingBorrow =
                                    borrowRepository.GetBorrowByCardAndBookId(returnCardId, returnBookId);
                                if (existingBorrow != null && existingBorrow.ReturnTime == 0)
                                {
                                    existingBorrow.ReturnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                                    try
                                    {
                                        borrowRepository.UpdateBorrowReturnTime(existingBorrow);
                                        Book bookToReturn = bookRepository.GetBookById(returnBookId);
                                        if (bookToReturn != null)
                                        {
                                            bookToReturn.Stock++;
                                            bookRepository.UpdateBook(bookToReturn); // 更新书籍库存
                                            Console.WriteLine("书籍归还成功。");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"警告：找不到 ID 为 {returnBookId} 的书籍，库存可能未正确更新。");
                                        }
                                    }
                                    catch (OdbcException ex)
                                    {
                                        Console.WriteLine($"归还失败：{ex.Message}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("没有找到对应的借阅记录或该书籍已归还。");
                                }
                            }
                            else
                            {
                                Console.WriteLine("书籍 ID 格式不正确。");
                            }
                        }
                        else
                        {
                            Console.WriteLine("借书证 ID 格式不正确。");
                        }

                        break;
                    case "b":
                        break;
                    default:
                        Console.WriteLine("无效的选择，请重新输入。");
                        break;
                }
            } while (borrowChoice != "b");
        }
    }
}