using System;
using System.IO;
using ContactsApp.Services;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(MvvmTest.iOS.SQLiteService))]

namespace MvvmTest.iOS
{
    public class SQLiteService : ISQLiteService
    {
        public static string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public SQLiteAsyncConnection GetConnection(string dbPath)
        {
            return new SQLiteAsyncConnection(GetLocalFilePath(dbPath));
        }
    }
}