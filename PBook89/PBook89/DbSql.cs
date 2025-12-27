using System.Data;
using System.Data.OleDb;

namespace PBook89
{
    class DbSql
    {
        // ✅ CORRECT DATABASE PATH
        private static string conStr =
            @"Provider=Microsoft.Jet.OLEDB.4.0;
              Data Source=C:\Users\mrvis\OneDrive\Desktop\project\ContactBookdb.mdb";

        // ✅ CONNECTION
        private static OleDbConnection GetConnection()
        {
            return new OleDbConnection(conStr);
        }

        // ✅ INSERT CONTACT
        public static void Insert(long phone, string name, string email, string address)
        {
            using (OleDbConnection con = GetConnection())
            {
                string sql = @"INSERT INTO ContactBook89
                               (PhoneNumber, FullName, Email, Address)
                               VALUES (?, ?, ?, ?)";

                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("?", phone);
                cmd.Parameters.AddWithValue("?", name);
                cmd.Parameters.AddWithValue("?", email);
                cmd.Parameters.AddWithValue("?", address);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ✅ DISPLAY ALL RECORDS
        public static DataTable DisplayAll()
        {
            using (OleDbConnection con = GetConnection())
            {
                OleDbDataAdapter da =
                    new OleDbDataAdapter("SELECT * FROM ContactBook89", con);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ✅ SEARCH BY PHONE NUMBER
        public static DataTable SearchByPhone(long phone)
        {
            using (OleDbConnection con = GetConnection())
            {
                string sql = "SELECT * FROM ContactBook89 WHERE PhoneNumber = ?";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("?", phone);

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ✅ DELETE BY ID
        public static void Delete(int id)
        {
            using (OleDbConnection con = GetConnection())
            {
                string sql = "DELETE FROM ContactBook89 WHERE ID = ?";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("?", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ✅ UPDATE BY ID
        public static void UpdateByID(int id, long phone, string name, string email, string address)
        {
            using (OleDbConnection con = GetConnection())
            {
                string sql = @"UPDATE ContactBook89
                               SET PhoneNumber = ?, FullName = ?, Email = ?, Address = ?
                               WHERE ID = ?";

                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("?", phone);
                cmd.Parameters.AddWithValue("?", name);
                cmd.Parameters.AddWithValue("?", email);
                cmd.Parameters.AddWithValue("?", address);
                cmd.Parameters.AddWithValue("?", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ✅ CHECK DUPLICATE PHONE NUMBER
        public static bool PhoneExists(long phone)
        {
            using (OleDbConnection con = GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM ContactBook89 WHERE PhoneNumber = ?";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("?", phone);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
