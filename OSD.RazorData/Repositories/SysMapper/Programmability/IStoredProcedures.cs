using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Programmability
{
    public interface IStoredProcedures
    {
        string ConnectionString { get; set; }

        void Execute(string name);
        void Execute(string name, object param);
        void ExecuteAsync(string name);
        void ExecuteAsync(string name, object param);
        void ExecuteTx(string name);
        void ExecuteTx(string name, object param);
        List<T> List<T>(string name);
        List<T> List<T>(string name, int id);
        List<T> List<T>(string name, object param);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> List<T1, T2, T3>(string name, object param);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string name, object param);
        Task<List<T>> ListAsync<T>(string name);
        void QueryExecute(string name);
        void QueryExecute(string name, object param);
        T Single<T>(string name, int id);
        T Single<T>(string name, object param);
    }
}