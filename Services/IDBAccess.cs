using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Services
{
    public interface IDBAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
        Task SaveBulk<T>(T parameters);
        //Task<IEnumerable<T2>> LoadData<T1, T2>(string v, object value);
    }
}
