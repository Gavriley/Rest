using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Rest.Models;

namespace Rest.Repository
{
    public class ConnectManager
    {
        private SqlConnection connection;
        private readonly string _connectionString;

        public ConnectManager()
        {
            _connectionString = "data source=.; database=Rest_db; integrated security=SSPI";
            connection = new SqlConnection(_connectionString);
        }

        public IEnumerable<Post> GetPosts()
        {
            List<Post> Posts = new List<Post>();

            SqlCommand command = new SqlCommand("GetPosts", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Posts.Add(
                        new Post
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreatedAt = reader.GetDateTime(3),
                            UpdatedAt = reader.GetDateTime(4)
                        }
                    );
            }

            connection.Close();

            return Posts;
        }

        public Post GetPostById(int id)
        {
            SqlCommand command = new SqlCommand("GetPostById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Post post = null;

            if (reader.Read())
            {
                post = new Post
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3),
                    UpdatedAt = reader.GetDateTime(4)
                };
            }
            connection.Close();

            return post;
        }

        public bool CreatePost(Post post)
        {
            SqlCommand command = new SqlCommand("CreatePost", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", post.Name);
            command.Parameters.AddWithValue("@Description", post.Description);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return (result >= 1) ? true : false;
        }

        public bool UpdatePost(Post post)
        {
            SqlCommand command = new SqlCommand("UpdatePost", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", post.Id);
            command.Parameters.AddWithValue("@Name", post.Name);
            command.Parameters.AddWithValue("@Description", post.Description);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return (result >= 1) ? true : false;
        }

        public bool DeletePost(int id)
        {
            SqlCommand command = new SqlCommand("DeletePost", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            
            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return (result >= 1) ? true : false;
        }
    }
}
