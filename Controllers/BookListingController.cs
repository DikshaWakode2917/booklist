namespace BooksListitingApis.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookListingController : Controller
    {
        
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\SHREE\\bookslist.mdf;Integrated Security=True;Connect Timeout=30;database=bookslist;user id=root;password=Diku@2917;"; 
           
            [HttpGet]
            public IEnumerable<Books> Get()
            {
                List<Books> books = new List<Books> { };
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM books;", connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Books book = new Books
                        {
                            Id = reader["Id"].ToString(),
                            Title = reader["Title"].ToString(),
                            Author = reader["Author"].ToString(),
                            ISBN = reader["ISBN"].ToString()
                        };
                        books.Add(book);
                    }
                    reader.Close();
                }
                return books;

            }

            [HttpGet("{id}")]
            public Books Get(int id)
            {
                Books book = null;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM books WHERE Id = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        book = new Books
                        {
                            Id = (string)reader["Id"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            ISBN = (string)reader["ISBN"]
                        };

                    }
                    reader.Close();
                }

                return book;
            }

            [HttpPost]
            public void Post([FromBody] Books book)
            {
                Guid myuuid = Guid.NewGuid();
                string id = myuuid.ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Books (Id, title, author, isbn) VALUES (@id, @title, @author, @isbn)");
                    command.Parameters.AddWithValue("@id", id); 
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@isbn", book.ISBN);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

            [HttpPut("{id}")]
            public void Put(int id, [FromBody] Books book)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("UPDATE books SET Title = @title, Author = @author, ISBN = @isbn WHERE Id = @id; ", connection);
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@isbn", book.ISBN);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

            [HttpDelete("{id}")]
            public void Delete(string id)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM books WHERE Id = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }

            }
        }
    }

