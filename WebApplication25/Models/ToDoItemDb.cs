using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WebApplication25.Models
{
    public class ToDoItemDb
    {
        private string _connectionString;

        public ToDoItemDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ToDoItem> GetAllNonCompletedItems()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM ToDoItems WHERE CompletedDate IS NULL";
            conn.Open();
            List<ToDoItem> items = new List<ToDoItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new ToDoItem
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    CategoryId = (int)reader["CategoryId"]
                });
            }
            return items;
        }

        public void MarkComplete(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE ToDoItems SET CompletedDate = GETDATE() WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
     
        public List<ToDoItem> GetAllCompletedItems()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM ToDoItems WHERE CompletedDate IS NOT NULL";
            conn.Open();
            List<ToDoItem> items = new List<ToDoItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new ToDoItem
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    CompletedDate = (DateTime)reader["CompletedDate"],
                    CategoryId = (int)reader["CategoryId"]
                });
            }
            return items;
        }

        public List<Category> GetAllCategories()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Categories";
            conn.Open();
            List<Category> categories = new List<Category>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    CategoryId = (int)reader["CategoryId"],
                    CategoryName = (string)reader["CategoryName"]
                });
            }
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Categories WHERE CategoryId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Category category = new Category
            {
                CategoryId = (int)reader["CategoryId"],
                CategoryName = (string)reader["CategoryName"]
            };
            return category;
        }

        public void EditCategory(int id, string name)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Categories SET CategoryName = @name WHERE CategoryId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void AddCategory(string name)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Categories (CategoryName)
                                VALUES(@categoryName)";
            cmd.Parameters.AddWithValue("@categoryName", name);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void AddToDoItem(string title, DateTime dueDate, int categoryId)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO ToDoItems (Title, DueDate, CategoryId)
                                VALUES(@title, @dueDate, @categoryId)";
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@dueDate", dueDate);
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<ToDoItem> GetItemsForCategory(int categoryId)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM ToDoItems t
                                JOIN Categories c   
                                ON t.CategoryId = c.CategoryId  
                                WHERE t.CategoryId = @categoryId";
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            conn.Open();
            List<ToDoItem> items = new List<ToDoItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new ToDoItem
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    CategoryId = (int)reader["CategoryId"]
                });
            }
            return items;
        }
    }
}
