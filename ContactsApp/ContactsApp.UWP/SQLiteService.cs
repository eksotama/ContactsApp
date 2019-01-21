using ContactsApp.Services;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactsApp.UWP.SQLiteService))]

namespace ContactsApp.UWP
{
    class SQLiteService : ISQLiteService
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = global::Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(path, filename);
        }

        public SQLiteAsyncConnection GetConnection(string dbPath)
        {
            return new SQLiteAsyncConnection(GetLocalFilePath(dbPath));
        }
    }
}
