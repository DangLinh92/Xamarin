using NuGet.Common;
using SmartGas.Models;
using SmartGas.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartGas.DataService
{
    public class SmartGasDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<SmartGasDatabase> Instance = new AsyncLazy<SmartGasDatabase>(async () =>
        {
            var instance = new SmartGasDatabase();
            CreateTableResult result = await Database.CreateTableAsync<OUT_PUT_INFO>();
            return instance;
        });

        public SmartGasDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<OUT_PUT_INFO>> GetItemsAsync()
        {
            return Database.Table<OUT_PUT_INFO>().ToListAsync();
        }

        public Task<List<OUT_PUT_INFO>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<OUT_PUT_INFO>("SELECT * FROM OUT_PUT_INFO WHERE isDelete = 'N'");
        }

        public Task<OUT_PUT_INFO> GetItemAsync(int id)
        {
            return Database.Table<OUT_PUT_INFO>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(OUT_PUT_INFO item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(OUT_PUT_INFO item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
