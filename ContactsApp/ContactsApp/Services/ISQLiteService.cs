using SQLite;

namespace ContactsApp.Services
{
    public interface ISQLiteService
    {
        SQLiteAsyncConnection GetConnection(string dbPath);
    }
}
