using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib.Model;

namespace RestItemService.Controllers.DBUtil
{
    public class ManageItems
    {
        private const string connString = @"Server=tcp:rasmus-item-dbserver.database.windows.net,1433;Initial Catalog=ItemDB;Persist Security Info=False;User ID=Rasmus;Password={Secret1234};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private const string GET_ALL = "select * from item";
        private const string GET_ID = "select * from item WHERE Id = @Id";
        private const string POST = "insert into item (Id, Name, Quality, Quantity) values (@Id, @Name, @Quality, @Quantity)";
        private const string PUT = "Update item set Id = @Id, Name = @Name, Quality = @Quality, Quantity = @Quantity";
        private const string DEL = "Delete from item WHERE Id = @Id";

        public IEnumerable<Item> Get()
        {
            List<Item> liste = new List<Item>();

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Item item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        public Item GetId(int id)
        {
            Item item = new Item();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ID, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        item = ReadNextElement(reader);
                    }
                }
            }

            return item;
        }

        public void Post(Item value)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(POST, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", value.Id);
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Quality", value.Quality);
                    cmd.Parameters.AddWithValue("@Quantity", value.Quantity);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Put(int id, Item value)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(PUT, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Quality", value.Quality);
                    cmd.Parameters.AddWithValue("@Quantity", value.Quantity);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using SqlConnection  conn = new SqlConnection(connString);
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(DEL, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #region Reader
        protected Item ReadNextElement(SqlDataReader reader)
        {
            Item item = new Item();

            item.Id = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            item.Quality = reader.GetString(2);
            item.Quantity = reader.GetDouble(3);

            return item;
        }
        #endregion
    }
}
