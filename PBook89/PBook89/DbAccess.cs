using System.Data.OleDb;

namespace PBook89
{
    class DbAccess
    {
        public static OleDbConnection GetConnection()
        {
            OleDbConnection con = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;
                  Data Source=C:\Users\mrvis\OneDrive\Desktop\project\ContactBookdb.mdb");
            return con;
        }
    }
}
