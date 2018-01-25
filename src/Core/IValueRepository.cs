using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IValuesRepository
    {
        Task<IEnumerable<Value>> GetAllValuesAsync();
        Task InsertAsync(Value value);
    }
}
