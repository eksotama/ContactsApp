using ContactsApp.Services;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(MvvmTest.Droid.SQLiteService))]

namespace MvvmTest.Droid
{
    public class SQLiteService : ISQLiteService
    {
        public static string GetLocalFilePath(string filename)
        {
            string documentsPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);

            return System.IO.Path.Combine(documentsPath, filename);
        }

        public SQLiteAsyncConnection GetConnection(string dbPath)
        {
            return new SQLiteAsyncConnection(GetLocalFilePath(dbPath));
        }
    }
}