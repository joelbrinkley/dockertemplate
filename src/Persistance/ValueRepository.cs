using Core;
using Core.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class ValueRepository : IValuesRepository
    {
        private readonly TemplateContext _context;
        private readonly ILog _log;

        public ValueRepository(TemplateContext context, ILog log)
        {
            this._context = context;
            this._log = log;
        }

        public async Task<IEnumerable<Value>> GetAllValuesAsync()
        {
            this._log.LogInfo("Getting all values async");
            var values = await _context.Values.ToListAsync();
            return values.Select(x => x.ToDomain());
        }

        public async Task InsertAsync(Value value)
        {
            this._log.LogInfo($"Inserting value: {value}");
            var valueEntity = value.ToEntity();
            this._context.Values.Add(valueEntity);
            await this._context.SaveChangesAsync();
        }
    }
}
