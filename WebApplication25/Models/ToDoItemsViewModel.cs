using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WebApplication25.Models
{
    public class ToDoItemsViewModel
    {
        private string _connectionString;
        public ToDoItemsViewModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ToDoItem> Items { get; set; }

        public string GetCategoryNameById(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT CategoryName from Categories WHERE CategoryId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            string catName = (string)cmd.ExecuteScalar();
            return catName;
        }
    }
}
